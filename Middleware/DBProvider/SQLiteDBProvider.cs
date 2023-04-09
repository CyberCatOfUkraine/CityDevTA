using AutoMapper;
using DatabaseWorker;

namespace Middleware.DBProvider
{
    public class SQLiteDBProvider : IDBProvider
    {
        private SQLiteDbManager _dbManager;
        private SQLiteDbContext _databaseContext;

        public SQLiteDBProvider()
        {
            _dbManager = new();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SQLiteMappingProfile>();
            });
            _databaseContext = new(new Mapper(configuration));
        }

        public SQLiteDbContext databaseContext => _databaseContext;

        IDatabaseContext IDBProvider.databaseContext => _databaseContext;

        public bool TryCreateDatabase()
        {
            return _dbManager.TryCreateDatabase();
        }

        public bool TryDeleteDatabase()
        {
            return _dbManager.TryDeleteDatabase();
        }
    }
}