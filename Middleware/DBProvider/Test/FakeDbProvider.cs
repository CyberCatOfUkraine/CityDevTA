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
    }
}