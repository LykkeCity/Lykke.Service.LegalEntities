using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Repositories
{
    public interface ISwiftCredentialsRepository
    {
        Task<SwiftCredentials> GetByIdAsync(string swiftCredentialsId);
        
        Task<IReadOnlyList<SwiftCredentials>> GetByLegalEntityIdAsync(string legalEntityId);

        Task<IReadOnlyList<SwiftCredentials>> FindAsync(string legalEntityId, string assetId);

        Task InsertAsync(SwiftCredentials swiftCredentials);

        Task UpdateAsync(SwiftCredentials swiftCredentials);

        Task DeleteAsync(string swiftCredentialsId);
    }
}
