using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Api;
using Lykke.Service.LegalEntities.Client.Models;
using Microsoft.Extensions.PlatformAbstractions;
using Refit;

namespace Lykke.Service.LegalEntities.Client
{
    public class LegalEntitiesClient : ILegalEntitiesClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IClientApi _clientApi;
        private readonly ILegalEntitiesApi _legalEntitiesApi;
        private readonly ISwiftCredentialsApi _swiftCredentialsApi;
        private readonly ApiRunner _runner;

        public LegalEntitiesClient(LegalEntitiesServiceClientSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrEmpty(settings.ServiceUrl))
                throw new ArgumentException("Service URL Required");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(settings.ServiceUrl),
                DefaultRequestHeaders =
                {
                    {
                        "User-Agent",
                        $"{PlatformServices.Default.Application.ApplicationName}/{PlatformServices.Default.Application.ApplicationVersion}"
                    }
                }
            };

            _clientApi = RestService.For<IClientApi>(_httpClient);
            _legalEntitiesApi = RestService.For<ILegalEntitiesApi>(_httpClient);
            _swiftCredentialsApi = RestService.For<ISwiftCredentialsApi>(_httpClient);

            _runner = new ApiRunner();
        }

        public async Task<ClientSwiftCredentialsModel> GetClientSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId)
        {
            return await _runner.RunAsync(() => _clientApi.GetSwiftCredentialsAsync(clientId, legalEntityId, assetId));
        }

        public async Task<IReadOnlyList<LegalEntityModel>> GetLegalEntitiesAllAsync()
        {
            return await _runner.RunAsync(() => _legalEntitiesApi.GetAllAsync());
        }

        public async Task<LegalEntityModel> GetLegalEntitiesByIdAsync(string legalEntityId)
        {
            return await _runner.RunAsync(() => _legalEntitiesApi.GetByIdAsync(legalEntityId));
        }

        public async Task AddLegalEntityAsync(CreateLegalEntityModel model)
        {
            await _runner.RunAsync(() => _legalEntitiesApi.AddAsync(model));
        }

        public async Task UpdateLegalEntityAsync(EditLegalEntityModel model)
        {
            await _runner.RunAsync(() => _legalEntitiesApi.UpdateAsync(model));
        }

        public async Task DeleteLegalEntityAsync(string legalEntityId)
        {
            await _runner.RunAsync(() => _legalEntitiesApi.DeleteAsync(legalEntityId));
        }

        public async Task<IReadOnlyList<SwiftCredentialsModel>> GetLegalEntitySwiftCredentialsAsync(string legalEntityId)
        {
            return await _runner.RunAsync(() => _legalEntitiesApi.GetSwiftCredentialsAsync(legalEntityId));
        }

        public async Task<ClientSwiftCredentialsModel> GetSwiftCredentialsByIdAsync(string swiftCredentialsId)
        {
            return await _runner.RunAsync(() => _swiftCredentialsApi.GetByIdAsync(swiftCredentialsId));
        }

        public async Task AddSwiftCredentialsAsync(CreateSwiftCredentialsModel model)
        {
            await _runner.RunAsync(() => _swiftCredentialsApi.AddAsync(model));
        }

        public async Task UpdateSwiftCredentialsAsync(EditSwiftCredentialsModel model)
        {
            await _runner.RunAsync(() => _swiftCredentialsApi.UpdateAsync(model));
        }

        public async Task DeleteSwiftCredentialsAsync(string swiftCredentialsId)
        {
            await _runner.RunAsync(() => _swiftCredentialsApi.DeleteAsync(swiftCredentialsId));
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
