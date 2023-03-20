using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class PartyService_GetAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public PartyService_GetAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task PartyService_GetAsync_CallsEfCoreAdd()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await partyService.GetAsync(ELECTION_ID);

        // Assert
        _mockDbContext.Verify(m => m.Party.FirstOrDefaultAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task PartyService_GetAsync_ReturnsCorrectType()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await partyService.GetAsync(ELECTION_ID);

        // Assert
        Assert.IsType<Party>(act);
    }
}