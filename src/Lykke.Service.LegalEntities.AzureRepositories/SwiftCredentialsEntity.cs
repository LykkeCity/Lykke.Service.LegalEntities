using Lykke.AzureStorage.Tables;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class SwiftCredentialsEntity : AzureTableEntity
    {
        public SwiftCredentialsEntity()
        {
        }

        public SwiftCredentialsEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        
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
