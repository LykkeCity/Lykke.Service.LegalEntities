using System;

namespace Lykke.Service.LegalEntities.Core.Exceptions
{
    public class LegalEntityNotFoundException : Exception
    {
        public LegalEntityNotFoundException()
        {
        }

        public LegalEntityNotFoundException(string legalEntityId)
            : base("Legal entity not found")
        {
            LegalEntityId = legalEntityId;
        }
        
        public string LegalEntityId { get; }
    }
}
