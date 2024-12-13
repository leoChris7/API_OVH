using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class EquipementDetailDTO
    {
        private int idEquipement;
        private String nomEquipement;
        private String dimensions; // L*l*H
        private String estActif;
        private SalleSansNavigationDTO salle;
        private TypeEquipementDTO typeEquipement;

        private decimal positionX;
        private decimal positionY;
        private decimal positionZ;

        [Required]
        public int IdEquipement { get => idEquipement; set => idEquipement = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomEquipement { get => nomEquipement; set => nomEquipement = value; }

        public string Dimensions { get => dimensions; set => dimensions = value; }

        [EtatValidationAttribute(ErrorMessage = "L'état doit être à OUI, NON ou NSP")]
        [Length(3, 3, ErrorMessage = "L'état doit être de 3 lettres")]
        public string EstActif { get => estActif; set => estActif = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal PositionX { get => positionX; set => positionX = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal PositionY { get => positionY; set => positionY = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal PositionZ { get => positionZ; set => positionZ = value; }

        public SalleSansNavigationDTO Salle { get => salle; set => salle = value; }

        public TypeEquipementDTO TypeEquipement { get => typeEquipement; set => typeEquipement = value; }
    }
}
