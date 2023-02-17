namespace Services;

public class ElectionService
{
    private readonly VotingContext _context;

    public ElectionService(VotingContext context)
    {
        _context = context;
    }

    public async Task<List<Election>> GetAll()
    {
        return await _context.Election.ToListAsync();
    }

    public async Task<Election?> GetAsync(int id)
    {
        return await _context.Election
            .Include(e => e.Parties)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Election> CreateAsync(Election election)
    {
        _context.Election.Add(election);
        await _context.SaveChangesAsync();
        return election;
    }

    public async Task<Election> UpdateAsync(Election election)
    {
        _context.Election.Update(election);
        await _context.SaveChangesAsync();
        return election;
    }
}