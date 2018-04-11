using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Exceptions;
using Lykke.Service.LegalEntities.Core.Repositories;
using Lykke.Service.LegalEntities.Core.Services;

namespace Lykke.Service.LegalEntities.Services
{
    public class SwiftCredentialsService : ISwiftCredentialsService
    {
        private readonly ISwiftCredentialsRepository _swiftCredentialsRepository;
        private readonly ILegalEntityRepository _legalEntityRepository;
        private readonly IClientSwiftCredentialsCache _clientSwiftCredentialsCache;
        private readonly ILog _log;

        public SwiftCredentialsService(
            ISwiftCredentialsRepository swiftCredentialsRepository,
            ILegalEntityRepository legalEntityRepository,
            IClientSwiftCredentialsCache clientSwiftCredentialsCache,
            ILog log)
        {
            _swiftCredentialsRepository = swiftCredentialsRepository;
            _legalEntityRepository = legalEntityRepository;
            _clientSwiftCredentialsCache = clientSwiftCredentialsCache;
            _log = log;
        }
        
        public Task<SwiftCredentials> GetByIdAsync(string swiftCredentialsId)
        {
            return _swiftCredentialsRepository.GetByIdAsync(swiftCredentialsId);
        }

        public Task<IReadOnlyList<SwiftCredentials>> GetByLegalEntityIdAsync(string legalEntityId)
        {
            return _swiftCredentialsRepository.GetByLegalEntityIdAsync(legalEntityId);
        }
       
        public async Task AddAsync(SwiftCredentials swiftCredentials)
        {
            var existingSwiftCredentials = await _swiftCredentialsRepository
                .FindAsync(swiftCredentials.LegalEntityId, swiftCredentials.AssetId);

            if (existingSwiftCredentials.Count > 0)
            {
                throw new SwiftCredentialsAlreadyExistsException(swiftCredentials.LegalEntityId,
                    swiftCredentials.AssetId);
            }

            var legalEntity = await _legalEntityRepository.GetByIdAsync(swiftCredentials.LegalEntityId);

            if(legalEntity == null)
            {
                throw new LegalEntityNotFoundException(swiftCredentials.LegalEntityId);
            }

            await _swiftCredentialsRepository.InsertAsync(swiftCredentials);
            
            await _log.WriteInfoAsync(nameof(SwiftCredentialsService), nameof(AddAsync),
                swiftCredentials.ToJson(), "Swift credentials added");
        }

        public async Task UpdateAsync(SwiftCredentials swiftCredentials)
        {
            var existingSwiftCredentials = await _swiftCredentialsRepository.GetByIdAsync(swiftCredentials.Id);

            if (existingSwiftCredentials == null)
            {
                throw new SwiftCredentialsNotFoundException(swiftCredentials.Id);
            }
            
            await _swiftCredentialsRepository.UpdateAsync(swiftCredentials);

            await _clientSwiftCredentialsCache.ClearAsync("Swift credentials updated");
            
            await _log.WriteInfoAsync(nameof(SwiftCredentialsService), nameof(UpdateAsync),
                swiftCredentials.ToJson(), "Swift credentials updated");
        }

        public async Task DeleteAsync(string swiftCredentialsId)
        {
            var swiftCredentials = await _swiftCredentialsRepository.GetByIdAsync(swiftCredentialsId);

            if(swiftCredentials == null)
                return;
           
            await _swiftCredentialsRepository.DeleteAsync(swiftCredentialsId);

            await _clientSwiftCredentialsCache.ClearAsync("Swift credentials deleted");

            await _log.WriteInfoAsync(nameof(SwiftCredentialsService), nameof(DeleteAsync),
                new {swiftCredentialsId}.ToJson(), "Swift credentials deleted");
        }
    }
}
