using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Services
{
    public interface IClientSwiftCredentialsCache
    {
        Task ClearAsync(string reason);
        Task SetAsync(string clientId, string legalEntityId, string assetId, ClientSwiftCredentials item);
        Task<ClientSwiftCredentials> GetAsync(string clientId, string legalEntityId, string assetId);
    }
}
