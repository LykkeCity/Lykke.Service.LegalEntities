using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Services
{
    public interface IClientService
    {
        Task<ClientSwiftCredentials> GetSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId);
    }
}
