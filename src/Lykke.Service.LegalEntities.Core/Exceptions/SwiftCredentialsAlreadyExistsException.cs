using System;

namespace Lykke.Service.LegalEntities.Core.Exceptions
{
    public class SwiftCredentialsAlreadyExistsException : Exception
    {
        public SwiftCredentialsAlreadyExistsException()
        {
        }

        public SwiftCredentialsAlreadyExistsException(string legalEntityId, string assetId)
            : base("Swift credentials already exists")
        {
            LegalEntityId = legalEntityId;
            AssetId = assetId;
        }
        
        public string LegalEntityId { get; }
        public string AssetId { get; }
    }
}
