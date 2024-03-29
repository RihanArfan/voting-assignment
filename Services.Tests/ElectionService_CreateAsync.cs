﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Tests;

public class ElectionService_CreateAsync
{
    private readonly Mock<VotingContext> _mockDbContext;

    public ElectionService_CreateAsync()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _mockDbContext = new Mock<VotingContext>(options);
    }

    [Fact]
    public async Task ElectionService_CreateAsync_CallsEfCoreAdd()
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
        var act = await electionService.CreateAsync(election);

        // Assert
        _mockDbContext.Verify(m => m.Election.Add(It.IsAny<Election>()), Times.Once);
    }


    [Fact]
    public async Task ElectionService_CreateAsync_ReturnsCorrectType()
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
        var act = await electionService.CreateAsync(election);

        // Assert
        Assert.IsType<Election>(act);
    }
}