namespace WebAPI.Repositories.Interfaces
{
    public interface IOnderwijsmoduleRepository<T> : IRepository<T>
    {
        Task<T?> GetWithRelationshipsById(int id);

        Task<bool> IncreaseVersion(int id);
    }
}
