using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IOnderwijsmoduleRepository<T> : IRepository<T>
    {
        Task<List<T>> GetAllOnderwijsmodulesViaOpleidingId(int opleidingId);

        Task<Onderwijsmodule?> GetOnderwijsmoduleVoorExportById(int id);

        Task VoegOnderwijseenheidToe(int id, Onderwijseenheid entity);

        Task<bool> IncreaseVersion(int id);
    }
}
