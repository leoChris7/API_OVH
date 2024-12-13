using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class CapteurSansNavigationDTO
    {
        [Required]
        public int IdCapteur { get; set; }

        public int? IdMur { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Le nom ne doit pas dépasser 25 caractères.")]
        public string NomCapteur { get; set; }

        [Required]
        [Length(3, 3, ErrorMessage = "L'état doit être de 3 caractères")]
        [EtatValidationAttribute(ErrorMessage = "Etat invalide")]
        public string EstActif { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal XCapteur { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal YCapteur { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal ZCapteur { get; set; }
    }
}
