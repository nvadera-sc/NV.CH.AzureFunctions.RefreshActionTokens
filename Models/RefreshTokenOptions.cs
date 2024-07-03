namespace NV.CH.AzureFunctions.RefreshActionTokens.Models;

public class RefreshActionTokensOptions
{
    public const string SettingsKey = "RefreshActionTokens";

    public IEnumerable<string> ActionIdentifiers { get; set; } = [];
}
