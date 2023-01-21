namespace WebAPI.Repositories.Interfaces
{
    public interface IOpleidingsprofielRepository<T> : IRepository<T>
    {
        Task<List<T>> GetAllOpleidingsprofielenByOpleidingId(int opleidingId);
    }
}
