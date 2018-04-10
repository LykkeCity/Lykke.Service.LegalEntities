using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.Core.Repositories
{
    public interface ILegalEntityRepository
    {
        Task<IReadOnlyList<LegalEntity>> GetAllAsync();
        
        Task<LegalEntity> GetByIdAsync(string legalEntityId);
        
        Task InsertAsync(LegalEntity legalEntity);
        
        Task UpdateAsync(LegalEntity legalEntity);
        
        Task DeleteAsync(string legalEntityId);
    }
}
