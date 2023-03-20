using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ElectionService_GetAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ElectionService_GetAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ElectionService_GetAsync_CallsEfCoreAdd()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await electionService.GetAsync(ELECTION_ID);

        // Assert
        _mockDbContext.Verify(m => m.Election.FirstOrDefaultAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ElectionService_GetAsync_ReturnsCorrectType()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await electionService.GetAsync(ELECTION_ID);

        // Assert
        Assert.IsType<Election>(act);
    }
}