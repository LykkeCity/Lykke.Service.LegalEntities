namespace Lykke.Service.LegalEntities.Client.Models
{
    public class ClientSwiftCredentialsModel
    {
        public string Bic { get; set; }
        
        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public string PurposeOfPayment { get; set; }

        public string BankAddress { get; set; }

        public string CompanyAddress { get; set; }

        public string CorrespondentAccount { get; set; }
    }
}
