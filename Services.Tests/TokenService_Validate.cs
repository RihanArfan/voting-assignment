using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Services.Tests;

public class TokenService_Validate
{
    [Fact]
    public void TokenService_Validate_PreventsNull()
    {
        var mockLogger = Mock.Of<ILogger<TokenService>>();
        var mockConfiguration = Mock.Of<IConfiguration>();
        var dbContext = new VotingContext(new DbContextOptionsBuilder<VotingContext>()
            .UseInMemoryDatabase(databaseName: "VotingContext")
            .Options);

        var tokenService = new TokenService(dbContext, mockLogger, mockConfiguration);
        var result = tokenService.Validate(null);

        Assert.False(result, "Null should not succeed");
    }
}