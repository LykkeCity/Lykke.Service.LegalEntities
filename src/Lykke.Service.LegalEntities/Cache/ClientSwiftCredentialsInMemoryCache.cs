using System.Collections.Concurrent;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Services;

namespace Lykke.Service.LegalEntities.Cache
{
    public class ClientSwiftCredentialsInMemoryCache : IClientSwiftCredentialsCache
    {
        private readonly ILog _log;

        private readonly ConcurrentDictionary<string, ClientSwiftCredentials> _storage =
            new ConcurrentDictionary<string, ClientSwiftCredentials>();

        public ClientSwiftCredentialsInMemoryCache(ILog log)
        {
            _log = log;
        }
        
        public async Task ClearAsync(string reason)
        {
            _storage.Clear();

            await _log.WriteInfoAsync(nameof(ClientSwiftCredentialsInMemoryCache), nameof(ClearAsync),
                "Cache invalidated", reason);
        }

        public Task SetAsync(string clientId, string legalEntityId, string assetId, ClientSwiftCredentials item)
        {
            _storage.AddOrUpdate(GetKey(clientId, legalEntityId, assetId), item, (key, oldValue) => item);
            return Task.CompletedTask;
        }

        public Task<ClientSwiftCredentials> GetAsync(string clientId, string legalEntityId, string assetId)
        {
            if (_storage.TryGetValue(GetKey(clientId, legalEntityId, assetId), out ClientSwiftCredentials cachedValue))
            {
                return Task.FromResult(cachedValue);
            }

            return Task.FromResult((ClientSwiftCredentials) null);
        }

        private string GetKey(string clientId, string legalEntityId, string assetId)
        {
            return $"{clientId}-{legalEntityId}-{assetId}";
        }
    }
}
