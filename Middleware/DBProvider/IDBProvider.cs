using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.DBProvider
{
    internal interface IDBProvider
    {
        bool TryCreateDatabase();

        bool TryDeleteDatabase();

        public IDatabaseContext databaseContext { get; }
    }
}