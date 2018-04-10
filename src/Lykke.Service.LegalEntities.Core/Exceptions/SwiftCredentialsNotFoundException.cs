using System;

namespace Lykke.Service.LegalEntities.Core.Exceptions
{
    public class SwiftCredentialsNotFoundException : Exception
    {
        public SwiftCredentialsNotFoundException()
        {
        }

        public SwiftCredentialsNotFoundException(string id)
            : base("Swift credentials entity not found")
        {
            Id = id;
        }
        
        public SwiftCredentialsNotFoundException(string legalEntityId, string assetId)
            : base("Swift credentials entity not found")
        {
            LegalEntityId = legalEntityId;
            AssetId = assetId;
        }
        
        public string Id { get; }
        
        public string LegalEntityId { get; }
        
        public string AssetId { get; }
    }
}
