using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using AzureStorage.Tables.Templates.Index;
using Common;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Repositories;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class SwiftCredentialsRepository : ISwiftCredentialsRepository
    {
        private readonly INoSQLTableStorage<SwiftCredentialsEntity> _storage;
        private readonly INoSQLTableStorage<AzureIndex> _indexIdStorage;

        public SwiftCredentialsRepository(
            INoSQLTableStorage<SwiftCredentialsEntity> storage,
            INoSQLTableStorage<AzureIndex> indexIdStorage)
        {
            _storage = storage;
            _indexIdStorage = indexIdStorage;
        }
        
        public async Task<SwiftCredentials> GetByIdAsync(string swiftCredentialsId)
        {
            var index = await _indexIdStorage.GetDataAsync(
                GetIndexIdPartitionKey(swiftCredentialsId),
                GetIndexIdRowKey(swiftCredentialsId));

            if (index == null)
                return null;
            
            var entity = await _storage.GetDataAsync(index);
            
            return Mapper.Map<SwiftCredentials>(entity);
        }
        
        public async Task<IReadOnlyList<SwiftCredentials>> GetByLegalEntityIdAsync(string legalEntityId)
        {
            var entities = await _storage.GetDataAsync(GetPartitionKey(legalEntityId));

            return Mapper.Map<List<SwiftCredentials>>(entities);
        }

        public async Task<IReadOnlyList<SwiftCredentials>> FindAsync(string legalEntityId, string assetId)
        {
            var entities = await _storage.GetDataAsync(GetPartitionKey(legalEntityId), o => o.AssetId == assetId);

            return Mapper.Map<List<SwiftCredentials>>(entities);
        }

        public async Task InsertAsync(SwiftCredentials swiftCredentials)
        {
            var entity = new SwiftCredentialsEntity(
                GetPartitionKey(swiftCredentials.LegalEntityId),
                GetRowKey())
            {
                LegalEntityId = swiftCredentials.LegalEntityId,
                AssetId = swiftCredentials.AssetId
            };

            Mapper.Map(swiftCredentials, entity);

            await _storage.InsertAsync(entity);

            await _indexIdStorage.InsertAsync(AzureIndex.Create(
                GetIndexIdPartitionKey(entity.RowKey), GetIndexIdRowKey(entity.RowKey), entity));
        }

        public async Task UpdateAsync(SwiftCredentials swiftCredentials)
        {
            var index = await _indexIdStorage.GetDataAsync(
                GetIndexIdPartitionKey(swiftCredentials.Id),
                GetIndexIdRowKey(swiftCredentials.Id));

            if(index == null)
                return;

            await _storage.MergeAsync(index.PrimaryPartitionKey, index.PrimaryRowKey, entity =>
            {
                Mapper.Map(swiftCredentials, entity);
                return entity;
            });
        }

        public async Task DeleteAsync(string swiftCredentialsId)
        {
            var index = await _indexIdStorage.GetDataAsync(
                GetIndexIdPartitionKey(swiftCredentialsId),
                GetIndexIdRowKey(swiftCredentialsId));

            if (index == null)
                return;

            await _storage.DeleteAsync(index);
            await _indexIdStorage.DeleteAsync(index);
        }

        private static string GetPartitionKey(string legalEntityId)
            => legalEntityId;

        private static string GetRowKey()
            => Guid.NewGuid().ToString("D");

        private static string GetIndexIdPartitionKey(string swiftCredentialsId)
            => $"IX_Id_{swiftCredentialsId.CalculateHexHash32(1)}";

        private static string GetIndexIdRowKey(string swiftCredentialsId)
            => swiftCredentialsId;
    }
}
