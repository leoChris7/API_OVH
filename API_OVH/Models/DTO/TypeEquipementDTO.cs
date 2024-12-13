using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class TypeEquipementDTO
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;

        [Required]
        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
    }
}
