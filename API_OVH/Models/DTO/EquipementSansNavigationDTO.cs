using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class EquipementSansNavigationDTO
    {
        [Required]
        public int IdEquipement { get; set; }

        public int IdMur { get; set; }

        public int IdTypeEquipement { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomEquipement { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "La longueur doit être comprise entre 0 et 999,999,999.9cm.")]
        public decimal Longueur { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "La largeur doit être comprise entre 0 et 999,999,999.9cm.")]
        public decimal Largeur { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "La hauteur doit être comprise entre 0 et 999,999,999.9cm.")]
        public decimal Hauteur { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal XEquipement { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal YEquipement { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal ZEquipement { get; set; }

        [EtatValidationAttribute(ErrorMessage = "L'état doit être à OUI, NON ou NSP")]
        [Length(3, 3, ErrorMessage = "L'état doit être de 3 lettres")]
        public string EstActif { get; set; }
    }
}
