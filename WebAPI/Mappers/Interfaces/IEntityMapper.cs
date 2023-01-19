namespace WebAPI.Mappers.Interfaces
{
    public interface IEntityMapper<EntityModel, DtoModel>
    {
        EntityModel MapToEntityModel(DtoModel dtoModel);
    }
}