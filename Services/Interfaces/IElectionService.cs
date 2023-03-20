namespace Services.Interfaces;

public interface IElectionService
{
    Task<List<Election>> GetAllAsync();
    Task<Election?> GetAsync(int id);
    Task<Election> CreateAsync(Election election);
    Task<Election> UpdateAsync(Election election);
    Task DeleteAsync(int id);
}