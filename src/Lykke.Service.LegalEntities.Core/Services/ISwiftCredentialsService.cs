using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Services
{
    public interface ISwiftCredentialsService
    {
        Task<SwiftCredentials> GetByIdAsync(string swiftCredentialsId);
        
        Task<IReadOnlyList<SwiftCredentials>> GetByLegalEntityIdAsync(string legalEntityId);

        Task AddAsync(SwiftCredentials swiftCredentials);
        
        Task UpdateAsync(SwiftCredentials swiftCredentials);
        
        Task DeleteAsync(string swiftCredentialsId);
    }
}
