﻿namespace Lykke.Service.LegalEntities.Client.Models
{
    public class SwiftCredentialsModel
    {
        public string Id { get; set; }
        
        public string LegalEntityId { get; set; }
        
        public string AssetId { get; set; }
        
        public string Bic { get; set; }
        
        public string AccountNumber { get; set; }
        
        public string AccountName { get; set; }
        
        public string PurposeOfPaymentFormat { get; set; }
        
        public string BankAddress { get; set; }
        
        public string CompanyAddress { get; set; }
        
        public string CorrespondentAccount { get; set; }
    }
}
