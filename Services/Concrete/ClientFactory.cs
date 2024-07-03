using Microsoft.Extensions.Options;
using NV.CH.AzureFunctions.RefreshActionTokens.Models;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;
using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Concrete;

public class ClientFactory(IOptions<ContentHubClientOptions> options) : IClientFactory
{
    private readonly ContentHubClientOptions _options = options.Value;

    public IWebMClient CreateClient()
    {
        var endpoint = new Uri(_options.Url);

        var oauth = new OAuthPasswordGrant
        {
            ClientId = _options.ClientId,
            ClientSecret = _options.ClientSecret,
            UserName = _options.Username,
            Password = _options.Password
        };

        return MClientFactory.CreateMClient(endpoint, oauth);
    }
}
