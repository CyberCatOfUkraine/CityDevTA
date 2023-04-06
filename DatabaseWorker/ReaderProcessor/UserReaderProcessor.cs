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
            (int id, string name) = (int.Parse(reader[0].ToString()!), reader[1].ToString()!);
            return new User(name) { Id = id };
        }
    }
}