using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

namespace Services.Tests;

public class VoteService_VoteAsync
{
    private readonly Mock<VotingContext> _dbContext;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IVoteService> _mockVoteService;

    public VoteService_VoteAsync()
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
    public async Task VoteService_VoteAsync_PreventInvalidTokens()
    {
        // Arrange
        var voteService = new VoteService(_dbContext.Object, _mockTokenService.Object, _mockElectionService.Object);

        // Act
        Func<Task> act = () => voteService.VoteAsync(string.Empty, string.Empty);

        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
    }

    [Fact]
    public async Task VoteService_VoteAsync_CallsIsUserVotedAsync()
    {
        // Arrange
        var partyId = "1";

        // create mock IElectionService
        var mockElectionService = new Mock<IElectionService>();

        // mock token service GetByValue to return token
        var token = new Token
        {
            Value = "token",
            Election = new Election()
        };
        _mockTokenService.Setup(x => x.GetByValue(It.IsAny<string>())).ReturnsAsync(token);

        var voteService = new VoteService(_dbContext.Object, _mockTokenService.Object, _mockElectionService.Object);

        // Act
        await voteService.VoteAsync(partyId, token.Value);

        // Assert
        // check if IsUserVotedAsync was called
        _mockVoteService.Verify(x => x.IsUserVotedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}