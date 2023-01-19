using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IGebruikerRepository<T> : IRepository<T>
    {
        Task<T?> GetUserByEmail(string email);
    }
}
