using Lykke.AzureStorage.Tables;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class LegalEntityEntity : AzureTableEntity
    {
        public LegalEntityEntity()
        {
        }
        
        public LegalEntityEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Address { get; set; }

        public string Regulation { get; set; }

        public string SupportPhone { get; set; }

        public string SupportEmail { get; set; }
    }
}
