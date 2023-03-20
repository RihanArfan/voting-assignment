using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ElectionService_DeleteAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ElectionService_DeleteAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ElectionService_DeleteAsync_CallsEfCoreRemove()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        await electionService.DeleteAsync(ELECTION_ID);

        // Assert
        _mockDbContext.Verify(m => m.Election.Remove(It.IsAny<Election>()), Times.Once);
    }
}