namespace Services.Interfaces;

public interface IVoteService
{
    Task VoteAsync(string partyId, string tokenValue);

    Task<bool> IsUserVotedAsync(int userId, int electionId);
}