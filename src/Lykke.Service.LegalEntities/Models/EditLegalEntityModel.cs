using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.LegalEntities.Models
{
    public class EditLegalEntityModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Address { get; set; }

        public string Regulation { get; set; }

        public string SupportPhone { get; set; }

        public string SupportEmail { get; set; }
    }
}
