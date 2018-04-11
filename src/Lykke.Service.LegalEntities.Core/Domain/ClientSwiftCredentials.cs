namespace Lykke.Service.LegalEntities.Core.Domain
{
    /// <summary>
    /// Represents a client specific swift credentials.
    /// </summary>
    public class ClientSwiftCredentials
    {
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
        /// The formated purpose of payment.
        /// </summary>
        public string PurposeOfPayment { get; set; }

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
