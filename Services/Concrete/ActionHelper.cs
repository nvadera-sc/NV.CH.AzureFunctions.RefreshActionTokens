using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;
using Stylelabs.M.Sdk.Contracts.Base;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Concrete;

public class ActionHelper : IActionHelper
{
    private const string Action_PropertyName_Settings = "Settings";
    private const string Action_PropertyName_Settings_Headers = "headers";

    public void UpdateAuthorizationHeader(IEntity actionEntity, string authorizationHeaderValue)
    {
        var actionSettings = actionEntity.GetPropertyValue<JToken>(Action_PropertyName_Settings);
        var headers = actionSettings[Action_PropertyName_Settings_Headers]?.ToObject<List<KeyValuePair<string, string>>>() ?? [];

        headers.Remove(headers.SingleOrDefault(x => x.Key == HeaderNames.Authorization));
        headers.Add(new KeyValuePair<string, string>(HeaderNames.Authorization, authorizationHeaderValue));

        actionSettings[Action_PropertyName_Settings_Headers] = JToken.FromObject(headers, GetSnakeCaseSerializer());
    }

    public void UpdateAuthorizationHeaders(IEnumerable<IEntity> actionEntities, string authorizationHeader)
    {
        foreach (var actionEntity in actionEntities)
        {
            UpdateAuthorizationHeader(actionEntity, authorizationHeader);
        }
    }

    private static JsonSerializer GetSnakeCaseSerializer()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }
        };
        return JsonSerializer.Create(serializerSettings);
    }
}
