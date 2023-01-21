using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IGebruikerRepository<T> : IRepository<T>
    {
        Task<T?> GetGebruikerByEmail(string email);

        Task AddGebruikerToKlas(int id, T entity);
    }
}
