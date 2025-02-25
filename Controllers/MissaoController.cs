using jwt_tokens.Autenticacao;
using jwt_tokens.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_tokens.Controllers;

[ApiController]
[Route("[controller]")]
public class MissaoController : ControllerBase
{
    private readonly ITokenManager _tokenManager;

    public MissaoController(ITokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    [HttpGet("hello-world")]
    public string HelloWorld() => "Hello World!";


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Heroi))
            return NotFound();

        var heroi = BancoHerois.Herois.FirstOrDefault(x => x.Nome == request.Heroi);

        if (heroi is null)
            return NotFound();

        var token = _tokenManager.GerarToken(heroi);
        var refreshToken = _tokenManager.GerarRefreshToken(heroi);

        return Ok(new LoginResponse(token, refreshToken));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest();

        var isValidTokenResult = await _tokenManager.ValidateTokenAsync(request.RefreshToken);

        if (!isValidTokenResult.isValid)
            return Unauthorized();

        var nomeHeroi = isValidTokenResult.nomeHeroi;
        var heroi = BancoHerois.Herois.FirstOrDefault(x => x.Nome == nomeHeroi);

        if (heroi is null)
            return Unauthorized();

        var token = _tokenManager.GerarToken(heroi);
        var refreshToken = _tokenManager.GerarRefreshToken(heroi);

        return Ok(new LoginResponse(token, refreshToken));
    }

    [Authorize]
    [HttpGet("somente-heroi")]
    public string SomenteHeroi() => "Você é Herói!";

    [Authorize(Roles = BancoHerois.PODE_VOAR)]
    [HttpGet("pode-voar")]
    public string PodeVoar() => "Missão completa";

    [Authorize(Roles = BancoHerois.SUPER_INTELIGENTE)]
    [HttpGet("investigar-crime")]
    public string InvestigarCrime() => "Missão completa";
}

public record LoginRequest(string Heroi);
public record RefreshTokenRequest(string RefreshToken);
public record LoginResponse(string Token, string RefreshToken);

