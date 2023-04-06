using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal class UserReaderProcessor : IReaderProcessor<User>
    {
        public User Process(DbDataReader reader)
        {
            var test = (reader[0] is DBNull ? -1 : reader[0], reader[1]);
            return new User(test.Item2.ToString()) { Id = int.Parse(test.Item1.ToString()) };
        }
    }
}