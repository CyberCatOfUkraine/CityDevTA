namespace Middleware.DBProvider
{
    public interface IDatabaseRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IEnumerable<T> GetAll();

        T GetById(int id);

        int Count();

        bool Exists(int id);
    }
}