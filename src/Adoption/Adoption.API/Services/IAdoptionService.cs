namespace Adoption.API.Services;

public interface IAdoptionService 
{
    Task<string> AdoptAsync(Guid petId, string userPhone);
}