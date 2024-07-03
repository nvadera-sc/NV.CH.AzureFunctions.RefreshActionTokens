using Stylelabs.M.Sdk.Contracts.Base;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

public interface IActionHelper
{
    void UpdateAuthorizationHeader(IEntity actionEntity, string authorizationHeaderValue);

    void UpdateAuthorizationHeaders(IEnumerable<IEntity> actionEntities, string authorizationHeaderValue);
}
