using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.DBProvider.Test
{
    public class FakeDbProvider : IDBProvider
    {
        public IDatabaseContext databaseContext => new FakeDatabaseContext();

        public static bool? DbIsCreated = false;

        public FakeDbProvider(Action dbCreatedCallback)
        {
            if (DbIsCreated != null && !DbIsCreated.Value)
            {
                dbCreatedCallback.Invoke();
                DbIsCreated = true;
            }
        }
    }
}