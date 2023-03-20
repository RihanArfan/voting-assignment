namespace Services;

public class PartiesService : IPartiesService
{
    private readonly VotingContext _context;

    public PartiesService(VotingContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     List all parties
    /// </summary>
    /// <returns></returns>
    public async Task<List<Party>> GetAllAsync()
    {
        return await _context.Party.ToListAsync();
    }

    /// <summary>
    ///     Get a specific party
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Party?> GetAsync(int id)
    {
        return await _context.Party
            .Include(e => e.Election)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    ///     Create a new party
    /// </summary>
    /// <param name="party"></param>
    /// <returns></returns>
    public async Task<Party> CreateAsync(Party party)
    {
        _context.Party.Add(party);
        await _context.SaveChangesAsync();
        return party;
    }

    /// <summary>
    ///     Update a party
    /// </summary>
    /// <param name="party"></param>
    /// <returns></returns>
    public async Task<Party> UpdateAsync(Party party)
    {
        _context.Party.Update(party);
        await _context.SaveChangesAsync();
        return party;
    }

    /// <summary>
    ///     Delete a party
    /// </summary>
    /// <param name="party"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var party = await GetAsync(id);

        // Prevent deleting parties associated with an election
        if (party.Election.Count > 0) throw new Exception("Cannot delete party with associated election");

        _context.Party.Remove(party);
        await _context.SaveChangesAsync();
    }
}