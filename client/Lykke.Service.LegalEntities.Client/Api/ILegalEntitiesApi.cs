using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;
using Refit;

namespace Lykke.Service.LegalEntities.Client.Api
{
    internal interface ILegalEntitiesApi
    {
        [Get("/api/legalentities")]
        Task<IReadOnlyList<LegalEntityModel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        [Get("/api/legalentities/{legalEntityId}")]
        Task<LegalEntityModel> GetByIdAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));

        [Post("/api/legalentities")]
        Task AddAsync(CreateLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken));

        [Put("/api/legalentities")]
        Task UpdateAsync(EditLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken));

        [Delete("/api/legalentities/{legalEntityId}")]
        Task DeleteAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));

        [Get("/api/legalentities/{legalEntityId}/swiftcredentials")]
        Task<IReadOnlyList<SwiftCredentialsModel>> GetSwiftCredentialsAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
