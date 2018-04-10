using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;
using Refit;

namespace Lykke.Service.LegalEntities.Client.Api
{
    internal interface ISwiftCredentialsApi
    {
        [Get("/api/swiftcredentials/{swiftCredentialsId}")]
        Task<ClientSwiftCredentialsModel> GetByIdAsync(string swiftCredentialsId);

        [Post("/api/swiftcredentials")]
        Task AddAsync(CreateSwiftCredentialsModel model);

        [Patch("/api/swiftcredentials")]
        Task UpdateAsync(EditSwiftCredentialsModel model);

        [Delete("/api/swiftcredentials/{swiftCredentialsId}")]
        Task DeleteAsync(string swiftCredentialsId);
    }
}
