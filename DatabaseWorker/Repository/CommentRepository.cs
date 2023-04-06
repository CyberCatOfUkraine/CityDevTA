using DatabaseWorker.Model;
using DatabaseWorker.ReaderProcessor;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.Repository
{
    public class CommentRepository : IRepository<Comment>
    {
        public void Add(Comment entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.InsertCommentQuery(entity));
        }

        public int Count()
        {
            var f = delegate (DbDataReader reader)
            {
                int count = int.Parse(reader[0].ToString()!);
                return count;
            };
            int count = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.GetRowsCountQuery(nameof(Comment)), f).First();

            return count;
        }

        public void Delete(Comment entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.RemoveByID(nameof(Comment), entity.Id));
        }

        public bool Exists(int id)
        {
            return SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(Comment), id)).HasRows;
        }

        public IEnumerable<Comment> GetAll()
        {
            var f = delegate (DbDataReader reader)
            {
                return new CommentReaderProcessor().Process(reader);
            };
            var comments = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.SelectAllFromQuery(nameof(Comment)), f);

            return comments;
        }

        public Comment GetById(int id)
        {
            var f = delegate (DbDataReader reader)
            {
                return new CommentReaderProcessor().Process(reader);
            };
            return f(SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(Comment), id)));
        }

        public void Update(Comment entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.UpdateCommentQuery(entity));
        }
    }
}