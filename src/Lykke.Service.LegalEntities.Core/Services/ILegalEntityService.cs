using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Services
{
    public interface ILegalEntityService
    {
        Task<IReadOnlyList<LegalEntity>> GetAllAsync();

        Task<LegalEntity> GetByIdAsync(string legalEntityId);

        Task AddAsync(LegalEntity legalEntity);
        
        Task UpdateAsync(LegalEntity legalEntity);
        
        Task DeleteAsync(string legalEntityId);
    }
}
