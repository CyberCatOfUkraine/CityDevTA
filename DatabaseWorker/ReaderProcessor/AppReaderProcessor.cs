using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal class AppReaderProcessor : IReaderProcessor<App>
    {
        public App Process(DbDataReader reader)
        {
            (int id, string name) = (int.Parse(reader[0].ToString()!), reader[1].ToString()!);
            return new App(name) { Id = id };
        }
    }
}