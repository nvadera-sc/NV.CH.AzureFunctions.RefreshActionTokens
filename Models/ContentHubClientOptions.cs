namespace NV.CH.AzureFunctions.RefreshActionTokens.Models;

public class ContentHubClientOptions
{
    public const string SettingsKey = "ContentHubClient";

    public string Url { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
