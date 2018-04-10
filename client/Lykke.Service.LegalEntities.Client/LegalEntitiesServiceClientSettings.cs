namespace Lykke.Service.LegalEntities.Client 
{
    /// <summary>
    /// Settings for <see cref="ILegalEntitiesClient"/>.
    /// </summary>
    public class LegalEntitiesServiceClientSettings
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LegalEntitiesServiceClientSettings"/> with service url.
        /// </summary>
        public LegalEntitiesServiceClientSettings(string serviceUrl)
        {
            ServiceUrl = serviceUrl;
        }

        /// <summary>
        /// The asset disclaimers service url.
        /// </summary>
        public string ServiceUrl {get; set;}
    }
}
