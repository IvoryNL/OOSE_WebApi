namespace WebAPI.Mappers.Interfaces
{
    public interface IMapper<TEntity, TDtoModel> : IEntityMapper<TEntity, TDtoModel>, IDtoMapper<TEntity, TDtoModel>
    {
    }
}
