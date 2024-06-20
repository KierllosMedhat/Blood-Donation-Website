namespace BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int? id);
        T GetByName(string name);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
