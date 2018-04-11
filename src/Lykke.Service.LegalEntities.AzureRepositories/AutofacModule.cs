using Autofac;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Common.Log;
using Lykke.Service.LegalEntities.Core.Repositories;
using Lykke.SettingsReader;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class AutofacModule : Module
    {
        private readonly IReloadingManager<string> _connectionString;
        private readonly ILog _log;

        public AutofacModule(IReloadingManager<string> connectionString, ILog log)
        {
            _connectionString = connectionString;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            const string legalEntitiesTableName = "LegalEntities";
            const string swiftCredentialsTableName = "SwiftCredentials";
            
            builder.RegisterInstance<ILegalEntityRepository>(new LegalEntityRepository(
                AzureTableStorage<LegalEntityEntity>.Create(_connectionString,
                    legalEntitiesTableName, _log)));

            builder.RegisterInstance<ISwiftCredentialsRepository>(new SwiftCredentialsRepository(
                AzureTableStorage<SwiftCredentialsEntity>.Create(_connectionString,
                    swiftCredentialsTableName, _log),
                AzureTableStorage<AzureIndex>.Create(_connectionString,
                    swiftCredentialsTableName, _log)));
        }
    }
}
