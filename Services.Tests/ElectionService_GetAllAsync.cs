using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ElectionService_GetAllAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ElectionService_GetAllAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ElectionService_GetAllAsync_CallsEfCoreToList()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);

        // Act
        var act = await electionService.GetAllAsync();

        // Assert
        _mockDbContext.Verify(m => m.Election.ToListAsync(CancellationToken.None), Times.Once);
    }


    [Fact]
    public async Task ElectionService_GetAllAsync_ReturnsCorrectType()
    {
        // Arrange
        var electionService = new ElectionService(_mockDbContext.Object);


        // Act
        var act = await electionService.GetAllAsync();

        // Assert
        Assert.IsType<List<Election>>(act);
    }
}