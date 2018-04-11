using Lykke.AzureQueueIntegration;

namespace Lykke.Service.LegalEntities.Settings.SlackNotifications
{
    public class SlackNotificationsSettings
    {
        public AzureQueueSettings AzureQueue { get; set; }
    }
}
