using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.TableCreator
{
    internal class SQLiteTableCreator
    {
        public void CreateTables()
        {
            try
            {
                SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.CreateAppTableTemplate);
                SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.CreateCommentTableQuery);
                SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.CreateUserTableQuery);
                SQLiteWorker.GetInstance().SendQuery(SQLiteTemplate.CreateRecordTableQuery);
            }
            catch (AggregateException e)
            {
                throw e.InnerException!;
            }
        }
    }
}