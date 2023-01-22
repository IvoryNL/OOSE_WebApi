using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface ILeeruitkomstRepository<T> : IRepository<T>
    {
        Task<List<Leeruitkomst>> GetLeeruitkomstenByOpleidingId(int id);
    }
}
