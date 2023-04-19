using AutoMapper;
using DatabaseWorker;

namespace Middleware.DBProvider
{
    public class SQLiteDBProvider : IDBProvider
    {
        private SQLiteDbManager _dbManager;
        private SQLiteDbContext _databaseContext;

        public SQLiteDBProvider(Action dbCreatedCallback)
        {
            _dbManager = new(dbCreatedCallback);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SQLiteMappingProfile>();
            });
            _databaseContext = new(new Mapper(configuration), _dbManager);
        }

        public SQLiteDbContext databaseContext => _databaseContext;

        IDatabaseContext IDBProvider.databaseContext => _databaseContext;
    }
}