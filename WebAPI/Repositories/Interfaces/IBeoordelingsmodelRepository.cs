using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Repositories.Interfaces
{
    public interface IBeoordelingsmodelRepository<T> : IRepository<T>
    {
        Task<T> GetBeoordelingsmodelByTentamenId(int id);
    }
}
