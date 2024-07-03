using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NV.CH.AzureFunctions.RefreshActionTokens.Models;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Concrete;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        services.AddTransient<IActionHelper, ActionHelper>()
            .AddTransient<IClientFactory, ClientFactory>()
            .AddTransient<IEntityHelper, EntityHelper>()
            .AddTransient<ITokenService, TokenService>();

        services.AddOptions<ContentHubClientOptions>().BindConfiguration(ContentHubClientOptions.SettingsKey);
        services.AddOptions<OAuthServerOptions>().BindConfiguration(OAuthServerOptions.SettingsKey);
        services.AddOptions<RefreshActionTokensOptions>().BindConfiguration(RefreshActionTokensOptions.SettingsKey);
    })
    .Build();

host.Run();