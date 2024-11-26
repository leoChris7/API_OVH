using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("TYPEEQUIPEMENT")]
    public partial class TypeEquipement
    {
        private ICollection<Equipement> equipements;

        [Key]
        [Column("IDTYPEEQUIPEMENT")]
        public int IdTypeEquipement { get; set; }

        [Required]
        [Column("NOMTYPEEQUIPEMENT", TypeName = "varchar(20)")]
        public string NomTypeEquipement { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Equipement.TypeEquipementNavigation))]
        public ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }
    }
}
