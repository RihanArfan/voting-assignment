using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Tests;

public class VoteService_IsUserVotedAsync
{
    private readonly Mock<VotingContext> _dbContext;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IVoteService> _mockVoteService;

    public VoteService_IsUserVotedAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _dbContext = new Mock<VotingContext>(options);

        _mockTokenService = new Mock<ITokenService>();
        _mockElectionService = new Mock<IElectionService>();
        _mockVoteService = new Mock<IVoteService>();
    }

    [Fact]
    public async Task VoteService_IsUserVotedAsync_CallsVoteInDbContext()
    {
        // Arrange
        // create mock IElectionService
        var mockElectionService = new Mock<IElectionService>();

        var voteService = new VoteService(_dbContext.Object, _mockTokenService.Object, _mockElectionService.Object);

        // Act
        await voteService.IsUserVotedAsync(1, 1);

        // Assert
        _dbContext.Verify(x => x.Vote, Times.Once);
    }
}