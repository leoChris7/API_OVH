using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("typeequipement")]
    public partial class TypeEquipement
    {
        private ICollection<Equipement> equipements;

        [Key]
        [Column("idtypeequipement")]
        public int IdTypeEquipement { get; set; }

        [Required]
        [Column("nomtypeequipement", TypeName = "varchar(20)")]
        public string NomTypeEquipement { get; set; }

        [InverseProperty(nameof(Equipement.TypeEquipementNavigation))]
        public ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }
    }
}
