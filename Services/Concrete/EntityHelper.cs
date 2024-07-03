using NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;
using Stylelabs.M.Sdk.Contracts.Base;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Concrete;

public class EntityHelper(IClientFactory clientFactory) : IEntityHelper
{
    private readonly IClientFactory _clientFactory = clientFactory;

    public async Task<IEntity> GetAsync(string identifier)
    {
        var client = _clientFactory.CreateClient();
        return await client.Entities.GetAsync(identifier);
    }

    public async Task<IEnumerable<IEntity>> GetManyAsync(IEnumerable<string> identifiers)
    {
        var client = _clientFactory.CreateClient();
        return await client.Entities.GetManyAsync(identifiers);
    }

    public async Task<long> SaveAsync(IEntity entity)
    {
        var client = _clientFactory.CreateClient();
        return await client.Entities.SaveAsync(entity);
    }

    public async Task<IEnumerable<long>> SaveManyAsync(IEnumerable<IEntity> entities)
    {
        var client = _clientFactory.CreateClient();
        return await Task.WhenAll(entities.Select(client.Entities.SaveAsync));
    }
}
