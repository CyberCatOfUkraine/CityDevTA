using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal class RecordReaderProcessor : IReaderProcessor<Record>
    {
        public Record Process(DbDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}