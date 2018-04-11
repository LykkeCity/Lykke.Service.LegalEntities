using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;
using Refit;

namespace Lykke.Service.LegalEntities.Client.Api
{
    internal interface IClientApi
    {
        [Get("/api/clients/{clientId}/swiftcredentials")]
        Task<ClientSwiftCredentialsModel> GetSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
