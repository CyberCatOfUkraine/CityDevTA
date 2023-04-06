namespace DatabaseWorker
{
    using DatabaseWorker.Model;
    using DatabaseWorker.ReaderProcessor;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Linq;

    public class SQLiteWorker
    {
        private const string ConnectionString = "Data Source=database.db;Version=3;";

        public void MakeQuery(string query, bool test)
        {
            MakeQuery(query).Wait();
        }

        public IEnumerable<User> MakeQuery(string query, int test)
        {
            var result = ToEnumerable(MakeQueryWithResponse(query, new UserReaderProcessor()));
            return result;
        }

        private IEnumerable<T> ToEnumerable<T>(IAsyncEnumerable<T> asyncEnumerable)
        {
            var list = new List<T>();

            var d = async delegate ()
            {
                await foreach (var item in asyncEnumerable)
                {
                    list.Add(item);
                }
            };
            d.Invoke();
            return list;
        }

        private async Task MakeQuery(string query)
        {
            using var sqliteConnection = new SQLiteConnection(ConnectionString);

            await sqliteConnection.OpenAsync();
            using var transaction = sqliteConnection.BeginTransaction();
            try
            {
                using var command = sqliteConnection.CreateCommand();
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

        private async IAsyncEnumerable<T> MakeQueryWithResponse<T>(string query, IReaderProcessor<T> readerProcessor) where T : class
        {
            using var sqliteConnection = new SQLiteConnection(ConnectionString);

            await sqliteConnection.OpenAsync();
            using var transaction = sqliteConnection.BeginTransaction();
            bool cancelationNeed = true;
            try
            {
                using var command = sqliteConnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;

                using (var reader = await command.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        yield return readerProcessor.Process(reader);
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
    }
}