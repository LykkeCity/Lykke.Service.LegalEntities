using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.LegalEntities.Models
{
    public class EditSwiftCredentialsModel
    {
        [Required]
        public string Id { get; set; }
        
        public string Bic { get; set; }
        
        public string AccountNumber { get; set; }
        
        public string AccountName { get; set; }
        
        public string PurposeOfPaymentFormat { get; set; }
        
        public string BankAddress { get; set; }
        
        public string CompanyAddress { get; set; }
        
        public string CorrespondentAccount { get; set; }
    }
}
