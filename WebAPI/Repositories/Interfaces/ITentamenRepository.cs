namespace WebAPI.Repositories.Interfaces
{
    public interface ITentamenRepository<T> : IRepository<T>
    {
        Task KoppelLeeruitkomstAanTentamen(int id, T entity);

        Task OntkoppelLeeruitkomstVanTentamen(int id, int leeruitkomstId);

        Task InplannenTentamen(int id, T entity);

        Task VerwijderPlanningVanTentamen(int id, int planningId);

        Task<List<T>> GetAllTentamensVanOnderwijsuitvoeringStudent(int id);

        Task<List<T>> GetAllTentamensZonderBeoordelingsmodel();

        Task<List<T>> GetAllTentamensZonderBeoordelingsmodelVoorWijziging(int beoordelingsmodelId);
    }
}
