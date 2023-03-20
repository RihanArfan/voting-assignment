using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interfaces;

namespace Services.Tests;

public class TokenService_Validate
{
    private readonly VotingContext _dbContext;
    private readonly IConfiguration _mockConfiguration;
    private readonly ILogger<ITokenService> _mockLogger;

    public TokenService_Validate()
    {
        var options = new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase("VotingContext")
            .Options;
        _dbContext = new VotingContext(options);

        _mockLogger = Mock.Of<ILogger<TokenService>>();
        _mockConfiguration = Mock.Of<IConfiguration>();
    }

    // prevent validation succeeding if token is null
    [Fact]
    public void TokenService_Validate_PreventsNull()
    {
        // Arrange
        var tokenService = new TokenService(_dbContext, _mockLogger, _mockConfiguration);

        // Act
        var result = tokenService.Validate(null);

        // Assert
        Assert.False(result, "Null should not succeed");
    }

    // prevent validation succeeding if token is after the election end date
    [Fact]
    public void TokenService_Validate_PreventIfAfterEndDate()
    {
        // Arrange
        var tokenService = new TokenService(_dbContext, _mockLogger, _mockConfiguration);

        var token = new Token
        {
            Election = new Election
            {
                EndDate = DateTime.Now.AddDays(-1)
            }
        };

        // Act
        var result = tokenService.Validate(token);

        // Assert
        Assert.False(result, "Token should not succeed if after end date");
    }
}