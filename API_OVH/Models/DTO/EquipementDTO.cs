using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class EquipementDTO
    {
        private int idEquipement;
        private string nomEquipement;
        private string dimensions; // Dimensions: LxlxH
        private string nomSalleEquipement;
        private string nomTypeEquipement;

        [Required]
        public int IdEquipement { get => idEquipement; set => idEquipement = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom de l'équipement ne doit pas dépasser 20 caractères.")]
        public string NomEquipement { get => nomEquipement; set => nomEquipement = value; }

        [MaxLength(20, ErrorMessage = "Le nom de la salle ne doit pas dépasser 20 caractères.")]
        public string NomSalleEquipement { get => nomSalleEquipement; set => nomSalleEquipement = value; }

        [MaxLength(20, ErrorMessage = "Le nom du type d'équipement ne doit pas dépasser 20 caractères.")]
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
        
        public string Dimensions { get => dimensions; set => dimensions = value; }
    }
}
