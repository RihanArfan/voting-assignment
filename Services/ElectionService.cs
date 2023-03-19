namespace Services;

public class ElectionService : IElectionService
{
    private readonly VotingContext _context;

    public ElectionService(VotingContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     List all elections
    /// </summary>
    /// <returns></returns>
    public async Task<List<Election>> GetAllAsync()
    {
        return await _context.Election.ToListAsync();
    }

    /// <summary>
    ///     Get a specific election
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Election?> GetAsync(int id)
    {
        return await _context.Election
            .Include(e => e.Parties)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    ///     Create a new election
    /// </summary>
    /// <param name="election"></param>
    /// <returns></returns>
    public async Task<Election> CreateAsync(Election election)
    {
        _context.Election.Add(election);
        await _context.SaveChangesAsync();
        return election;
    }

    /// <summary>
    ///     Update an election
    /// </summary>
    /// <param name="election"></param>
    /// <returns></returns>
    public async Task<Election> UpdateAsync(Election election)
    {
        // TODO: Prevent updating linked parties after election has started

        _context.Election.Update(election);
        await _context.SaveChangesAsync();
        return election;
    }
}