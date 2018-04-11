﻿namespace Lykke.Service.LegalEntities.Client.Models
{
    public class CreateLegalEntityModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Address { get; set; }

        public string Regulation { get; set; }

        public string SupportPhone { get; set; }

        public string SupportEmail { get; set; }
    }
}
