using Lykke.Service.LegalEntities.Settings.Clients;
using Lykke.Service.LegalEntities.Settings.ServiceSettings;
using Lykke.Service.LegalEntities.Settings.SlackNotifications;
using Lykke.Service.PersonalData.Settings;

namespace Lykke.Service.LegalEntities.Settings
{
    public class AppSettings
    {
        public LegalEntitiesSettings LegalEntitiesService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
        public PersonalDataServiceClientSettings PersonalDataServiceClient { get; set; }
        public AssetsServiceClientSettings AssetsServiceClient { get; set; }
    }
}
