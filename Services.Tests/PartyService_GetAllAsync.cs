using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class PartyService_GetAllAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public PartyService_GetAllAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task PartyService_GetAllAsync_CallsEfCoreToList()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);

        // Act
        var act = await partyService.GetAllAsync();

        // Assert
        _mockDbContext.Verify(m => m.Party.ToListAsync(CancellationToken.None), Times.Once);
    }


    [Fact]
    public async Task PartyService_GetAllAsync_ReturnsCorrectType()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);

        // Act
        var act = await partyService.GetAllAsync();

        // Assert
        Assert.IsType<List<Party>>(act);
    }
}