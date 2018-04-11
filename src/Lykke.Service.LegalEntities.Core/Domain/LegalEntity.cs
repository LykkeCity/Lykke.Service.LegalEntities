namespace Lykke.Service.LegalEntities.Core.Domain
{
    /// <summary>
    /// Represents a Lykke legal entity.
    /// </summary>
    public class LegalEntity
    {
        /// <summary>
        /// The human readable identifier.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The legal entity name. 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The legal entity description. 
        /// </summary> 
        public string Description { get; set; }
        
        /// <summary>
        /// The legal address.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// The regulation details.
        /// </summary>
        public string Regulation { get; set; }
        
        /// <summary>
        /// The legal entity support phone.
        /// </summary>
        public string SupportPhone { get; set; }
        
        /// <summary>
        /// The legal entity support email.
        /// </summary>
        public string SupportEmail { get; set; }
    }
}
