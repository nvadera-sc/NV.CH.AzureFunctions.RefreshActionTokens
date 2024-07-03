namespace NV.CH.AzureFunctions.RefreshActionTokens.Models;

public class OAuthServerOptions
{
    public const string SettingsKey = "OAuthServer";

    public string Url { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public IEnumerable<string> Scopes { get; set; } = [];
}
