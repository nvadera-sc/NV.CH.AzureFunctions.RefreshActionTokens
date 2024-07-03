using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using NV.CH.AzureFunctions.RefreshActionTokens.Models;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Concrete;

public class TokenService(ILogger<TokenService> logger, IOptions<OAuthServerOptions> options) : ITokenService
{
    private readonly ILogger<TokenService> _logger = logger;
    private readonly OAuthServerOptions _options = options.Value;

    public async Task<string> GetAuthorizationHeaderAsync()
    {
        var token = await GetTokenAsync();
        _logger.LogInformation($"Acquired new token. Expires on {token.ExpiresOn}");
        return token.CreateAuthorizationHeader();
    }

    private async Task<AuthenticationResult> GetTokenAsync()
    {
        var app = ConfidentialClientApplicationBuilder.Create(_options.ClientId)
                    .WithClientSecret(_options.ClientSecret)
                    .WithAuthority(new Uri(_options.Url))
                    .Build();

        return await app.AcquireTokenForClient(_options.Scopes).ExecuteAsync();
    }
}
