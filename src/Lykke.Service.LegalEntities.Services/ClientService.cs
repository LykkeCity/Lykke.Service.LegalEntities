using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.Assets.Client;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Repositories;
using Lykke.Service.LegalEntities.Core.Services;
using Lykke.Service.PersonalData.Contract;

namespace Lykke.Service.LegalEntities.Services
{
    public class ClientService : IClientService
    {
        private readonly ISwiftCredentialsRepository _swiftCredentialsRepository;
        private readonly IPersonalDataService _personalDataService;
        private readonly IAssetsServiceWithCache _assetsServiceWithCache;
        private readonly IClientSwiftCredentialsCache _clientSwiftCredentialsCache;
        private readonly ILog _log;

        public ClientService(
            ISwiftCredentialsRepository swiftCredentialsRepository,
            IPersonalDataService personalDataService,
            IAssetsServiceWithCache assetsServiceWithCache,
            IClientSwiftCredentialsCache clientSwiftCredentialsCache,
            ILog log)
        {
            _swiftCredentialsRepository = swiftCredentialsRepository;
            _personalDataService = personalDataService;
            _assetsServiceWithCache = assetsServiceWithCache;
            _clientSwiftCredentialsCache = clientSwiftCredentialsCache;
            _log = log;
        }
        
        public async Task<ClientSwiftCredentials> GetSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId)
        {
            var clientSwiftCredentials = await _clientSwiftCredentialsCache.GetAsync(clientId, legalEntityId, assetId);

            if (clientSwiftCredentials != null)
                return clientSwiftCredentials;
            
            var asset = await _assetsServiceWithCache.TryGetAssetAsync(assetId);
            
            if (asset == null)
            {
                await _log.WriteWarningAsync(nameof(ClientService), nameof(GetSwiftCredentialsAsync),
                    new { clientId, legalEntityId, assetId }.ToJson(), "Asset not found.");
                
                return null;
            }
            
            var swiftCredentialsList = await _swiftCredentialsRepository.FindAsync(legalEntityId, assetId);

            if (swiftCredentialsList.Count > 1)
            {
                await _log.WriteWarningAsync(nameof(ClientService), nameof(GetSwiftCredentialsAsync),
                    new { legalEntityId, assetId }.ToJson(), "Ambiguous swift credentials.");
                
                return null;
            }

            var swiftCredentials = swiftCredentialsList.FirstOrDefault();
            
            if (swiftCredentials == null)
            {
                await _log.WriteWarningAsync(nameof(ClientService), nameof(GetSwiftCredentialsAsync),
                    new { clientId, legalEntityId, assetId }.ToJson(), "Swift credentials not found.");
                
                return null;
            }

            var personalData = await _personalDataService.GetAsync(clientId);
            
            if (personalData == null)
            {
                await _log.WriteWarningAsync(nameof(ClientService), nameof(GetSwiftCredentialsAsync),
                    new { clientId, legalEntityId, assetId }.ToJson(), "Personal data not found.");
                
                return null;
            }

            var assetTitle = string.IsNullOrEmpty(asset.DisplayId) ? asset.Id : asset.DisplayId;

            var purposeOfPayment = string.Format(swiftCredentials.PurposeOfPaymentFormat, assetTitle,
                personalData.Email?.Replace("@", "."));

            clientSwiftCredentials = new ClientSwiftCredentials
            {
                Bic = swiftCredentials.Bic,
                AccountName = swiftCredentials.AccountName,
                AccountNumber = swiftCredentials.AccountNumber,
                PurposeOfPayment = purposeOfPayment,
                BankAddress = swiftCredentials.BankAddress,
                CompanyAddress = swiftCredentials.CompanyAddress,
                CorrespondentAccount = swiftCredentials.CorrespondentAccount
            };

            await _clientSwiftCredentialsCache.SetAsync(clientId, legalEntityId, assetId, clientSwiftCredentials);

            return clientSwiftCredentials;
        }
    }
}
