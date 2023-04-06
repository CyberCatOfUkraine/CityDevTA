using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal interface IReaderProcessor<T> where T : class
    {
        public T Process(DbDataReader reader);
    }
}