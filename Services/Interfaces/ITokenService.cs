namespace Services.Interfaces;

public interface ITokenService
{
    Token Create(User user, Election election);

    Task<Token?> GetByValue(string tokenValue);

    bool Validate(Token? token);
}