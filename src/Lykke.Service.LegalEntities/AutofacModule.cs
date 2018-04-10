using System;
using Autofac;
using Common.Log;
using Lykke.Service.Assets.Client;
using Lykke.Service.LegalEntities.Cache;
using Lykke.Service.LegalEntities.Core.Services;
using Lykke.Service.LegalEntities.Settings.Clients;
using Lykke.Service.PersonalData.Client;
using Lykke.Service.PersonalData.Contract;
using Lykke.Service.PersonalData.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.LegalEntities
{
    public class AutofacModule : Module
    {
        private readonly PersonalDataServiceClientSettings _personalDataServiceClientSettings;
        private readonly ILog _log;

        public AutofacModule(
            PersonalDataServiceClientSettings personalDataServiceClientSettings,
            AssetsServiceClientSettings assetsServiceClientSettings,
            TimeSpan assetsCacheExpirationPeriod,
            IServiceCollection services,
            ILog log)
        {
            _personalDataServiceClientSettings = personalDataServiceClientSettings;
            _log = log;

            services.RegisterAssetsClient(AssetServiceSettings.Create(
                new Uri(assetsServiceClientSettings.ServiceUrl), assetsCacheExpirationPeriod));
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();
            
            builder.RegisterType<PersonalDataService>()
                .WithParameter(TypedParameter.From(_personalDataServiceClientSettings))
                .As<IPersonalDataService>()
                .SingleInstance();
            
            builder.RegisterType<ClientSwiftCredentialsInMemoryCache>()
                .As<IClientSwiftCredentialsCache>()
                .SingleInstance();
        }
    }
}
