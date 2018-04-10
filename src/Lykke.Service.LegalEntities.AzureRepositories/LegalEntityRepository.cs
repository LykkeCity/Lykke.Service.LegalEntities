using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Repositories;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class LegalEntityRepository : ILegalEntityRepository
    {
        private readonly INoSQLTableStorage<LegalEntityEntity> _storage;

        public LegalEntityRepository(INoSQLTableStorage<LegalEntityEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyList<LegalEntity>> GetAllAsync()
        {
            var entities = await _storage.GetDataAsync();

            return Mapper.Map<List<LegalEntity>>(entities);
        }

        public async Task<LegalEntity> GetByIdAsync(string legalEntityId)
        {
            var entity = await _storage.GetDataAsync(GetPartitionKey(), GetRowKey(legalEntityId));

            return Mapper.Map<LegalEntity>(entity);
        }

        public async Task InsertAsync(LegalEntity legalEntity)
        {
            var entity = new LegalEntityEntity(GetPartitionKey(), GetRowKey(legalEntity.Id));

            Mapper.Map(legalEntity, entity);

            await _storage.InsertAsync(entity);
        }

        public async Task UpdateAsync(LegalEntity legalEntity)
        {
            await _storage.MergeAsync(GetPartitionKey(), GetRowKey(legalEntity.Id), entity =>
            {
                Mapper.Map(legalEntity, entity);
                return entity;
            });
        }

        public Task DeleteAsync(string legalEntityId)
        {
            return _storage.DeleteAsync(GetPartitionKey(), GetRowKey(legalEntityId));
        }

        private static string GetPartitionKey()
            => "LegalEntity";

        private static string GetRowKey(string legalEntityId)
            => legalEntityId.ToUpper();
    }
}
