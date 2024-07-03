using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NV.CH.AzureFunctions.RefreshActionTokens.Models;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

namespace NV.CH.AzureFunctions.RefreshActionTokens
{
    public class RefreshActionTokens(ILoggerFactory loggerFactory,
                                     IOptions<RefreshActionTokensOptions> refreshActionTokensOptions,
                                     ITokenService tokenService,
                                     IEntityHelper entityHelper,
                                     IActionHelper actionHelper)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<RefreshActionTokens>();
        private readonly RefreshActionTokensOptions _refreshActionTokensOptions = refreshActionTokensOptions.Value;
        private readonly IEntityHelper _entityHelper = entityHelper;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IActionHelper _actionHelper = actionHelper;

        [Function("RefreshActionTokens")]
        public async Task Run([TimerTrigger("0 * * * *")] TimerInfo timer)
        {
            _logger.LogInformation($"Refresh Action Tokens function executed at: {DateTime.Now}");

            var authorizationHeader = await _tokenService.GetAuthorizationHeaderAsync();
            var actionEntities = await _entityHelper.GetManyAsync(_refreshActionTokensOptions.ActionIdentifiers);

            _actionHelper.UpdateAuthorizationHeaders(actionEntities, authorizationHeader);
            await _entityHelper.SaveManyAsync(actionEntities);

            _logger.LogInformation($"Tokens updated for actions {string.Join(',', actionEntities.Select(e => e.Identifier))}");

            if (timer?.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {timer.ScheduleStatus.Next}");
            }
        }
    }
}
