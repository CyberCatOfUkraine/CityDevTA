namespace DatabaseWorker
{
    using DatabaseWorker.Model;
    using DatabaseWorker.ReaderProcessor;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Linq;
    using System.Reflection.PortableExecutable;

    internal class SQLiteWorker
    {
        private const string DbName = "database.db";
        private const string ConnectionString = $"Data Source={DbName};Version=3;";

        private SQLiteConnection _connection;

        private static SQLiteWorker _SQLiteWorker = new();

        public static string GetDbFileName => DbName;

        public static SQLiteWorker GetInstance()
        {
            return _SQLiteWorker;
        }

        public SQLiteWorker()
        {
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
        }

        ~SQLiteWorker()
        {
            var connection = _connection;

            connection?.Close();
            connection?.Dispose();
        }

        internal void SendQuery(string query)
        {
            try
            {
                MakeQuery(query).Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? e;
            }
        }

        internal List<T> GetList<T>(string query, Func<DbDataReader, T> processor)
        {
            var list = new List<T>();

            try
            {
                IAsyncEnumerable<T> asyncEnumerable = MakeQueryWithResponse(query, processor);

                var d = async delegate ()
                {
                    await foreach (var item in asyncEnumerable)
                    {
                        list.Add(item);
                    }
                };
                d.Invoke().Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? e;
            }
            return list;
        }

        private async Task MakeQuery(string query)
        {
            using var transaction = _connection.BeginTransaction();
            try
            {
                using var command = _connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;

                await command.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        private async IAsyncEnumerable<T> MakeQueryWithResponse<T>(string query, Func<DbDataReader, T> processor)
        {
            using var transaction = _connection.BeginTransaction();
            bool cancelationNeed = true;
            try
            {
                using var command = _connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        yield return processor(reader);
                    }
                }

                transaction.Commit();
                cancelationNeed = false;
            }
            finally
            {
                if (cancelationNeed)
                {
                    transaction.Rollback();
                }
            }
        }

        internal T? GetDbDataReader<T>(string query, Func<DbDataReader, T> func)
        {
            var items = GetList(query, func);
            if (items.Count > 0)
            {
                return items.First();
            }
            return default;
        }
    }
}