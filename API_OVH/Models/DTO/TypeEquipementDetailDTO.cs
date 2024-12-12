using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class TypeEquipementDetailDTO
    {
        private ICollection<EquipementSansNavigationDTO> equipements;

        [Key]
        [Column("idtypeequipement")]
        public int IdTypeEquipement { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        [Column("nomtypeequipement", TypeName = "varchar(20)")]
        public string NomTypeEquipement { get; set; }

        [InverseProperty(nameof(Equipement.TypeEquipementNavigation))]
        public ICollection<EquipementSansNavigationDTO> Equipements { get => equipements; set => equipements = value; }
    }
}
