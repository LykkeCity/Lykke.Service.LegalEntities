using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;
using Refit;

namespace Lykke.Service.LegalEntities.Client.Api
{
    internal interface ISwiftCredentialsApi
    {
        [Get("/api/swiftcredentials/{swiftCredentialsId}")]
        Task<ClientSwiftCredentialsModel> GetByIdAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken));

        [Post("/api/swiftcredentials")]
        Task AddAsync(CreateSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken));

        [Patch("/api/swiftcredentials")]
        Task UpdateAsync(EditSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken));

        [Delete("/api/swiftcredentials/{swiftCredentialsId}")]
        Task DeleteAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
