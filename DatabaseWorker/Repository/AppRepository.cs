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
    public class AppRepository : IRepository<App>
    {
        public void Add(App entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.InsertAppQuery(entity));
        }

        public int Count()
        {
            var f = delegate (DbDataReader reader)
            {
                int count = int.Parse(reader[0].ToString()!);
                return count;
            };
            int count = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.GetRowsCountQuery(nameof(App)), f).First();

            return count;
        }

        public void Delete(App entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.RemoveByID(nameof(App), entity.Id));
        }

        public bool Exists(int id)
        {
            return SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(App), id)).HasRows;
        }

        public IEnumerable<App> GetAll()
        {
            var f = delegate (DbDataReader reader)
            {
                return new AppReaderProcessor().Process(reader);
            };
            var apps = SQLiteWorker.GetInstance().GetList(SQLiteTemplate.SelectAllFromQuery(nameof(App)), f);

            return apps;
        }

        public App GetById(int id)
        {
            var f = delegate (DbDataReader reader)
            {
                return new AppReaderProcessor().Process(reader);
            };
            return f(SQLiteWorker.GetInstance().GetDbDataReader(SQLiteTemplate.GetByIdQuery(nameof(App), id)));
        }

        public void Update(App entity)
        {
            SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.UpdateAppQuery(entity));
        }
    }
}