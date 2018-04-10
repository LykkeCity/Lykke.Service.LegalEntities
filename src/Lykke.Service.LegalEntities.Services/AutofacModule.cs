using Autofac;
using Lykke.Service.LegalEntities.Core.Services;

namespace Lykke.Service.LegalEntities.Services
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();
            
            builder.RegisterType<ClientService>()
                .As<IClientService>();
            
            builder.RegisterType<LegalEntityService>()
                .As<ILegalEntityService>();
            
            builder.RegisterType<SwiftCredentialsService>()
                .As<ISwiftCredentialsService>();
        }
    }
}
