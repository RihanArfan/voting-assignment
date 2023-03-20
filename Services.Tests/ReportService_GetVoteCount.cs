using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ReportService_GetVoteCount
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ReportService_GetVoteCount()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ReportService_GetVoteCount_CallsEfCoreCount()
    {
        // Arrange
        var reportService = new ReportService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await reportService.GetVoteCount(ELECTION_ID);

        // Assert
        _mockDbContext.Verify(m => m.Party.Count(), Times.Once);
    }

    [Fact]
    public async Task ReportService_GetVoteCount_ReturnsCorrectType()
    {
        // Arrange
        var reportService = new ReportService(_mockDbContext.Object);
        var ELECTION_ID = 1;

        // Act
        var act = await reportService.GetVoteCount(ELECTION_ID);

        // Assert
        Assert.IsType<int>(act);
    }
}