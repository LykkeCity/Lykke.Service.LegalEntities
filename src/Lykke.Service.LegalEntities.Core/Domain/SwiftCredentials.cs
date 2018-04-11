namespace Lykke.Service.LegalEntities.Core.Domain
{
    /// <summary>
    /// Represents a swift credentials of leagal entity for asset.
    /// </summary>
    public class SwiftCredentials
    {
        /// <summary>
        /// The swift credentials id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The legal entity id.
        /// </summary>
        public string LegalEntityId { get; set; }
        
        /// <summary>
        /// The asset id.
        /// </summary>
        public string AssetId { get; set; }
        
        /// <summary>
        /// The bank bic.
        /// </summary>
        public string Bic { get; set; }
        
        /// <summary>
        /// The bank account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The bank account name.
        /// </summary>
        public string AccountName { get; set; }
        
        /// <summary>
        /// The format of purpose of payment.
        /// </summary>
        /// <example>
        /// "Lykke Shares (coins) purchase {0} {1}"
        /// </example>
        public string PurposeOfPaymentFormat { get; set; }

        /// <summary>
        /// The bank address.
        /// </summary>
        public string BankAddress { get; set; }
        
        /// <summary>
        /// The company address.
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// The correspondent account.
        /// </summary>
        public string CorrespondentAccount { get; set; }
    }
}
