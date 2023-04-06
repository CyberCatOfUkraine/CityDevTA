using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal class CommentReaderProcessor : IReaderProcessor<Comment>
    {
        public Comment Process(DbDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}