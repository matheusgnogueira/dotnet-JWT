using jwt_tokens.Db;

namespace jwt_tokens.Autenticacao;

public interface ITokenManager
{
    string GerarToken(Heroi heroi);
    string GerarRefreshToken(Heroi heroi);
    Task<(bool isValid, string? nomeHeroi)> ValidateTokenAsync(string token);
}
