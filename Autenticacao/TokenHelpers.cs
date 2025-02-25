using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwt_tokens.Autenticacao;

internal static class TokenHelpers
{
    public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        // Obtém a chave secreta de maneira segura
        var secretKey = configuration["JwtSettings:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey is missing. Configure it in User Secrets or Environment Variables.");
        }

        var tokenKey = Encoding.UTF8.GetBytes(secretKey);

        return new TokenValidationParameters
        {
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidAudience = configuration["JwtSettings:Audience"],
            ValidateIssuer = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
        };
    }
}
