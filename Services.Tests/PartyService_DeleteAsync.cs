using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class PartyService_DeleteAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public PartyService_DeleteAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task PartyService_DeleteAsync_CallsEfCoreRemove()
    {
        // Arrange
        var electionService = new PartyService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        await electionService.DeleteAsync(ELECTION_ID);

        // Assert
        _mockDbContext.Verify(m => m.Party.Remove(It.IsAny<Party>()), Times.Once);
    }
}