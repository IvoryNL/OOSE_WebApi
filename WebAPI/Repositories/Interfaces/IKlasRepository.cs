namespace WebAPI.Repositories.Interfaces
{
    public interface IKlasRepository<T> : IRepository<T>
    {
        Task<List<T>> GetKlassenByOpleidingId(int opleidingId);
    }
}
