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
    public class RecordRepository : IRepository<Record>
    {
        public void Add(Record entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.InsertRecordQuery(entity));
        }

        public int Count()
        {
            var f = delegate (DbDataReader reader)
            {
                int count = int.Parse(reader[0].ToString()!);
                return count;
            };
            int count = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.GetRowsCountQuery(nameof(Record)), f).First();

            return count;
        }

        public void Delete(Record entity)
        {
            if (new AppRepository().Exists(entity.App.Id))
            {
                new AppRepository().Delete(entity.App);
            }
            if (new UserRepository().Exists(entity.User.Id))
            {
                new UserRepository().Delete(entity.User);
            }
            if (new CommentRepository().Exists(entity.Comment.Id))
            {
                new CommentRepository().Delete(entity.Comment);
            }
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.RemoveByID(nameof(Record), entity.Id));
        }

        public bool Exists(int id)
        {
            return SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(Record), id)).HasRows;
        }

        public IEnumerable<Record> GetAll()
        {
            var appEnumerable = new AppRepository().GetAll();
            var userEnumerable = new UserRepository().GetAll();
            var commentEnumerable = new CommentRepository().GetAll();

            var f = delegate (DbDataReader reader)
            {
                var record = new RecordReaderProcessor().Process(reader);
                record.App.Name = appEnumerable.First(x => x.Id == record.App.Id).Name;
                record.User.Name = userEnumerable.First(x => x.Id == record.User.Id).Name;

                var comment = commentEnumerable.First(x => x.Id == record.Comment.Id);
                record.Comment.Text = comment.Text;
                record.Comment.TimeStamp = comment.TimeStamp;

                return record;
            };
            var records = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.SelectAllFromQuery(nameof(Record)), f);

            return records;
        }

        public Record GetById(int id)
        {
            var f = delegate (DbDataReader reader)
            {
                var record = new RecordReaderProcessor().Process(reader);
                record.App.Name = new AppRepository().GetById(record.App.Id).Name;
                record.User.Name = new UserRepository().GetById(record.User.Id).Name;

                var comment = new CommentRepository().GetById(record.Comment.Id);
                record.Comment.Text = comment.Text;
                record.Comment.TimeStamp = comment.TimeStamp;

                return record;
            };
            var records = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.SelectAllFromQuery(nameof(Record)), f);
            return f(SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(Record), id)));
        }

        public void Update(Record entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.UpdateRecordQuery(entity));
        }
    }
}