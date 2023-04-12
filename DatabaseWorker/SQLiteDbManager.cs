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
        public SQLiteDbManager(Action callback)
        {
            if (!File.Exists(SQLiteWorker.GetDbFileName))
            {
                new SQLiteTableCreator().CreateTables();
                callback.Invoke();
            }
        }

        public IRepository<App> AppRepository => new AppRepository();
        public IRepository<User> UserRepository => new UserRepository();
        public IRepository<Comment> CommentRepository => new CommentRepository();
        public IRepository<Record> RecordRepository => new RecordRepository();
    }
}