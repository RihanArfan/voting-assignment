using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ElectionService_UpdateAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ElectionService_UpdateAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ElectionService_UpdateAsync_CallsEfCoreUpdate()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);
        
        var election = new Election
        {
            Id = 1,
            Name = "Test Election",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
        };

        // Act
        var act = await electionService.UpdateAsync(election);

        // Assert
        _mockDbContext.Verify(m => m.Election.Update(It.IsAny<Election>()), Times.Once);
    }

    [Fact]
    public async Task ElectionService_UpdateAsync_ReturnsCorrectType()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);
        
        var election = new Election
        {
            Id = 1,
            Name = "Test Election",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
        };

        // Act
        var act = await electionService.UpdateAsync(election);

        // Assert
        Assert.IsType<Election>(act);
    }
}