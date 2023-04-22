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
            int count = SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetRowsCountQuery(nameof(Record)), f);

            return count;
        }

        public void Delete(Record entity)
        {
            if (new AppRepository().GetById(entity.App.Id) != null)
            {
                new AppRepository().Delete(entity.App);
            }
            if (new UserRepository().GetById(entity.User.Id) != null)
            {
                new UserRepository().Delete(entity.User);
            }
            if (new CommentRepository().GetById(entity.Comment.Id) != null)
            {
                new CommentRepository().Delete(entity.Comment);
            }
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.RemoveByID(nameof(Record), entity.Id));
        }

        public bool Exists(int id)
        {
            return GetById(id) == null;
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
            return SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(Record), id), f);
        }

        public void Update(Record entity)
        {
            var appRepo = new AppRepository();
            var usrRepo = new UserRepository();
            var cmntRepo = new CommentRepository();

            var allApp = appRepo.GetAll();
            var allUsr = usrRepo.GetAll();
            var allCmnt = cmntRepo.GetAll();

            var app = appRepo!.GetById(entity.App.Id);
            if (appRepo!.Exists(entity.App.Id))
            {
                throw new InvalidOperationException("Неможливо оновити запис, додатку з таким індексом не існує");
            }
            if (usrRepo!.Exists(entity.User.Id))
            {
                throw new InvalidOperationException("Неможливо оновити запис, користувача з таким індексом не існує");
            }
            if (cmntRepo!.Exists(entity.Comment.Id))
            {
                throw new InvalidOperationException("Неможливо оновити запис, коментаря з таким індексом не існує");
            }
            appRepo.Update(entity.App);
            usrRepo.Update(entity.User);
            cmntRepo.Update(entity.Comment);

            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.UpdateRecordQuery(entity));
        }
    }
}