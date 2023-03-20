using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class PartyService_UpdateAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public PartyService_UpdateAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task PartyService_UpdateAsync_CallsEfCoreUpdate()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        
        var party = new Party
        {
            Id = 1,
            Name = "Test Party",
            Logo = "https://example.com/logo.png",
            Color = "#000000",
        };

        // Act
        var act = await partyService.UpdateAsync(party);

        // Assert
        _mockDbContext.Verify(m => m.Party.Update(It.IsAny<Party>()), Times.Once);
    }

    [Fact]
    public async Task PartyService_UpdateAsync_ReturnsCorrectType()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        
        var party = new Party
        {
            Id = 1,
            Name = "Test Party",
            Logo = "https://example.com/logo.png",
            Color = "#000000",
        };

        // Act
        var act = await partyService.UpdateAsync(party);

        // Assert
        Assert.IsType<Party>(act);
    }
}