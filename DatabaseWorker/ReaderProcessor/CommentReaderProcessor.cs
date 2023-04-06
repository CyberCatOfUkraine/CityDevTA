using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.ReaderProcessor
{
    internal class CommentReaderProcessor : IReaderProcessor<Comment>
    {
        public Comment Process(DbDataReader reader)
        {
            (int id, string text, DateTime timeStamp) = (int.Parse(reader[0].ToString()!), reader[1].ToString()!, DateTime.Parse(reader[2].ToString()!, CultureInfo.InvariantCulture));
            return new Comment(text, timeStamp) { Id = id };
        }
    }
}