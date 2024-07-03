using Stylelabs.M.Sdk.WebClient;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

public interface IClientFactory
{
    IWebMClient CreateClient();
}
