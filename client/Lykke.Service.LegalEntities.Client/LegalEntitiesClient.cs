using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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

        public Task<ClientSwiftCredentialsModel> GetClientSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _clientApi.GetSwiftCredentialsAsync(clientId, legalEntityId, assetId, cancellationToken));

        public Task<IReadOnlyList<LegalEntityModel>> GetLegalEntitiesAllAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.GetAllAsync(cancellationToken));

        public Task<LegalEntityModel> GetLegalEntitiesByIdAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.GetByIdAsync(legalEntityId, cancellationToken));

        public Task AddLegalEntityAsync(CreateLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.AddAsync(model, cancellationToken));

        public Task UpdateLegalEntityAsync(EditLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.UpdateAsync(model, cancellationToken));

        public Task DeleteLegalEntityAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.DeleteAsync(legalEntityId, cancellationToken));

        public Task<IReadOnlyList<SwiftCredentialsModel>> GetLegalEntitySwiftCredentialsAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _legalEntitiesApi.GetSwiftCredentialsAsync(legalEntityId, cancellationToken));

        public Task<ClientSwiftCredentialsModel> GetSwiftCredentialsByIdAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _swiftCredentialsApi.GetByIdAsync(swiftCredentialsId, cancellationToken));

        public Task AddSwiftCredentialsAsync(CreateSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _swiftCredentialsApi.AddAsync(model, cancellationToken));

        public Task UpdateSwiftCredentialsAsync(EditSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _swiftCredentialsApi.UpdateAsync(model, cancellationToken));

        public Task DeleteSwiftCredentialsAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken))
            => _runner.RunAsync(() => _swiftCredentialsApi.DeleteAsync(swiftCredentialsId, cancellationToken));

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
