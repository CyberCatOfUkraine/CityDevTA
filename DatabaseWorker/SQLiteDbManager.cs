using DatabaseWorker.Model;
using DatabaseWorker.Repository;
using DatabaseWorker.TableCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker
{
    public class SQLiteDbManager
    {
        private bool DatabaseExist => File.Exists(SQLiteWorker.GetDbFileName);

        public bool TryCreateDatabase()
        {
            var result = false;
            if (!DatabaseExist)
            {
                new SQLiteTableCreator().CreateTables();
                result = true;
            }
            return result;
        }

        public bool TryDeleteDatabase()
        {
            var result = false;
            if (DatabaseExist)
            {
                File.Delete(SQLiteWorker.GetDbFileName);
                result = true;
            }
            return result;
        }

        public IRepository<App> AppRepository => new AppRepository();
        public IRepository<User> UserRepository => new UserRepository();
        public IRepository<Comment> CommentRepository => new CommentRepository();
        public IRepository<Record> RecordRepository => new RecordRepository();
    }
}