using Stylelabs.M.Sdk.Contracts.Base;

namespace NV.CH.AzureFunctions.RefreshActionTokens.Services.Abstract;

public interface IEntityHelper
{
    Task<IEntity> GetAsync(string identifier);

    Task<IEnumerable<IEntity>> GetManyAsync(IEnumerable<string> identifiers);

    Task<long> SaveAsync(IEntity entity);

    Task<IEnumerable<long>> SaveManyAsync(IEnumerable<IEntity> entities);
}
