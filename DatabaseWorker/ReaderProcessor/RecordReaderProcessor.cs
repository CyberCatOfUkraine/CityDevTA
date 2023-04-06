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
        /// <summary>
        /// Увага! Ініціалізує запис елементами що містять лише власний Id
        /// </summary>
        /// <param name="reader">Об'єкт що повертається з БД</param>
        /// <returns></returns>
        public Record Process(DbDataReader reader)
        {
            (int id, int appId, int userId, int commentId) = (int.Parse(reader[0].ToString()!), int.Parse(reader[1].ToString()!), int.Parse(reader[2].ToString()!), int.Parse(reader[3].ToString()!));
            return new Record(new App(string.Empty) { Id = appId }, new User(string.Empty) { Id = userId }, new Comment(string.Empty, default) { Id = commentId }) { Id = id };
        }
    }
}