namespace Services.Interfaces;

public interface IElectionService
{
    Task<List<Election>> GetAll();
    Task<Election?> GetAsync(int id);
    Task<Election> CreateAsync(Election election);
    Task<Election> UpdateAsync(Election election);
}