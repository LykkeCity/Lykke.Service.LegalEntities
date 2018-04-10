using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;
using Refit;

namespace Lykke.Service.LegalEntities.Client.Api
{
    internal interface ILegalEntitiesApi
    {
        [Get("/api/legalentities")]
        Task<IReadOnlyList<LegalEntityModel>> GetAllAsync();

        [Get("/api/legalentities/{legalEntityId}")]
        Task<LegalEntityModel> GetByIdAsync(string legalEntityId);

        [Post("/api/legalentities")]
        Task AddAsync(CreateLegalEntityModel model);

        [Put("/api/legalentities")]
        Task UpdateAsync(EditLegalEntityModel model);

        [Delete("/api/legalentities/{legalEntityId}")]
        Task DeleteAsync(string legalEntityId);

        [Get("/api/legalentities/{legalEntityId}/swiftcredentials")]
        Task<IReadOnlyList<SwiftCredentialsModel>> GetSwiftCredentialsAsync(string legalEntityId);
    }
}
