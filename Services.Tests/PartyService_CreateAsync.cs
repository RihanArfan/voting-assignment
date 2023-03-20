using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class PartyService_CreateAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public PartyService_CreateAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task PartyService_CreateAsync_CallsEfCoreAdd()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        
        var party = new Party
        {
            Id = 1,
            Name = "Test Election",
            Logo = "https://example.com/logo.png",
            Color = "#000000",
        };

        // Act
        var act = await partyService.CreateAsync(party);

        // Assert
        _mockDbContext.Verify(m => m.Election.Add(It.IsAny<Election>()), Times.Once);
    }


    [Fact]
    public async Task PartyService_CreateAsync_ReturnsCorrectType()
    {
        // Arrange
        var partyService = new PartyService(_mockDbContext.Object);
        
        var party = new Party
        {
            Id = 1,
            Name = "Test Election",
            Logo = "https://example.com/logo.png",
            Color = "#000000",
        };

        // Act
        var act = await partyService.CreateAsync(party);

        // Assert
        Assert.IsType<Election>(act);
    }
}