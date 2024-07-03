namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

public interface ITokenService
{
    Task<string> GetAuthorizationHeaderAsync();
}
