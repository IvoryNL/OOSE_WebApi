namespace WebAPI.Mappers.Interfaces
{
    public interface IDtoMapper<EntityModel, DtoModel>
    {
        DtoModel MapToDtoModel(EntityModel entityModel);
    }
}