namespace WebAPI.Repositories.Interfaces
{
    public interface ILesRepository<T> : IRepository<T>
    {
        Task KoppelLesmateriaalAanLes(int id, T entity);

        Task OntkoppelLesmateriaalVanLes(int id, int lesmateriaalId);

        Task KoppelLeeruitkomstAanLes(int id, T entity);

        Task OntkoppelLeeruitkomstVanLes(int id, int leeruitkomstId);

        Task InplannenLes(int id, T entity);

        Task VerwijderPlanningVanLes(int id, int planningId);
    }
}
