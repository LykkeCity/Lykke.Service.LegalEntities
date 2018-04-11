using System;
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
    public class LegalEntityService : ILegalEntityService
    {
        private readonly ILegalEntityRepository _legalEntityRepository;
        private readonly ISwiftCredentialsRepository _swiftCredentialsRepository;
        private readonly ILog _log;

        public LegalEntityService(
            ILegalEntityRepository legalEntityRepository,
            ISwiftCredentialsRepository swiftCredentialsRepository,
            ILog log)
        {
            _legalEntityRepository = legalEntityRepository;
            _swiftCredentialsRepository = swiftCredentialsRepository;
            _log = log;
        }
        
        public Task<IReadOnlyList<LegalEntity>> GetAllAsync()
        {
            return _legalEntityRepository.GetAllAsync();
        }

        public Task<LegalEntity> GetByIdAsync(string legalEntityId)
        {
            return _legalEntityRepository.GetByIdAsync(legalEntityId);
        }

        public async Task AddAsync(LegalEntity legalEntity)
        {
            var existingLegalEntity = await _legalEntityRepository.GetByIdAsync(legalEntity.Id);

            if (existingLegalEntity != null)
                throw new LegalEntityAlreadyExistsException(legalEntity.Id);

            await _legalEntityRepository.InsertAsync(legalEntity);

            await _log.WriteInfoAsync(nameof(LegalEntityService), nameof(AddAsync), legalEntity.ToJson(),
                "Legal entity added");
        }

        public async Task UpdateAsync(LegalEntity legalEntity)
        {
            var existingLegalEntity = await _legalEntityRepository.GetByIdAsync(legalEntity.Id);

            if (existingLegalEntity == null)
                throw new LegalEntityNotFoundException(legalEntity.Id);

            await _legalEntityRepository.UpdateAsync(legalEntity);
            
            await _log.WriteInfoAsync(nameof(LegalEntityService), nameof(UpdateAsync), legalEntity.ToJson(),
                "Legal entity updated");
        }

        public async Task DeleteAsync(string legalEntityId)
        {
            var swiftCredentialses = await _swiftCredentialsRepository.GetByLegalEntityIdAsync(legalEntityId);

            if(swiftCredentialses.Count > 0)
                throw new InvalidOperationException("One or more swift credentials associated with legal entity");

            await _legalEntityRepository.DeleteAsync(legalEntityId);

            await _log.WriteInfoAsync(nameof(LegalEntityService), nameof(DeleteAsync), new {legalEntityId}.ToJson(),
                "Legal entity deleted");
        }
    }
}
