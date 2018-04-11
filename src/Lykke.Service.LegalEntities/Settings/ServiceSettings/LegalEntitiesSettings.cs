using System;
using Lykke.Service.LegalEntities.Settings.ServiceSettings.Db;

namespace Lykke.Service.LegalEntities.Settings.ServiceSettings
{
    public class LegalEntitiesSettings
    {
        public DbSettings Db { get; set; }

        public TimeSpan AssetsCacheExpirationPeriod { get; set; }
    }
}
