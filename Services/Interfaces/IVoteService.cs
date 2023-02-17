namespace Services.Interfaces;

public interface IVoteService
{
    Task VoteAsync(Vote vote);

    Task<bool> IsUserVotedAsync(int userId, int electionId);
}