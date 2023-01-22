namespace WebAPI.Repositories.Interfaces
{
    public interface ILesRepository<T> : IRepository<T>
    {
        Task AddLesmateriaalToLes(int id, T entity);

        Task RemoveLesmateriaalFromLes(int id, int lesmateriaalId);

        Task AddLeeruitkomstToLes(int id, T entity);

        Task RemoveLeeruitkomstFromLes(int id, int leeruitkomstId);

        Task AddLesToPlanning(int id, T entity);

        Task RemoveLesFromPlanning(int id, int planningId);
    }
}
