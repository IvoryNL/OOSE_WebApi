namespace WebAPI.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetAll();
        Task Create(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
