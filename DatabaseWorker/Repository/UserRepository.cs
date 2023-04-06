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
    public class UserRepository : IRepository<User>
    {
        public void Add(User entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.InsertUserQuery(entity));
        }

        public int Count()
        {
            var f = delegate (DbDataReader reader)
            {
                int count = int.Parse(reader[0].ToString()!);
                return count;
            };
            int count = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.GetRowsCountQuery(nameof(User)), f).First();

            return count;
        }

        public void Delete(User entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.RemoveByID(nameof(User), entity.Id));
        }

        public bool Exists(int id)
        {
            return SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(User), id)).HasRows;
        }

        public IEnumerable<User> GetAll()
        {
            var f = delegate (DbDataReader reader)
            {
                return new UserReaderProcessor().Process(reader);
            };
            var users = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.SelectAllFromQuery(nameof(User)), f);

            return users;
        }

        public User GetById(int id)
        {
            var f = delegate (DbDataReader reader)
            {
                return new UserReaderProcessor().Process(reader);
            };
            return f(SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(User), id)));
        }

        public void Update(User entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.UpdateUserQuery(entity));
        }
    }
}