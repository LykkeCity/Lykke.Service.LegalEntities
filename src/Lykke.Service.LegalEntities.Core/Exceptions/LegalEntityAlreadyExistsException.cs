using System;

namespace Lykke.Service.LegalEntities.Core.Exceptions
{
    public class LegalEntityAlreadyExistsException : Exception
    {
        public LegalEntityAlreadyExistsException()
        {
        }

        public LegalEntityAlreadyExistsException(string legalEntityId)
            : base("Legal entity already exists")
        {
            LegalEntityId = legalEntityId;
        }
        
        public string LegalEntityId { get; }
    }
}
