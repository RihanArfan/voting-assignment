namespace Services.Interfaces;

public interface IPartyService
{
    Task<List<Party>> GetAllAsync();
    Task<Party?> GetAsync(int id);
    Task<Party> CreateAsync(Party party);
    Task<Party> UpdateAsync(Party party);
    Task DeleteAsync(int id);
}