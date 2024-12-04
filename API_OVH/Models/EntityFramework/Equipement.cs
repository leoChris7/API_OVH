using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("equipement")]
    public partial class Equipement
    {
        private TypeEquipement typeEquipementNavigation;
        private Mur murNavigation;

        [Key]
        [Column("idequipement")] 
        public int IdEquipement { get; set; }

        [Column("idmur")]
        public int IdMur { get; set; }

        [Column("idtypeequipement")]
        public int IdTypeEquipement { get; set; }

        [Required]
        [Column("nomequipement", TypeName = "varchar(20)")]
        public string NomEquipement { get; set; }

        [Column("longueur", TypeName = "numeric")]
        public decimal Longueur { get; set; } = 0;

        [Column("largeur", TypeName = "numeric")]
        public decimal Largeur { get; set; } = 0;

        [Column("hauteur", TypeName = "numeric")]
        public decimal Hauteur { get; set; } = 0;

        [Column("xequipement", TypeName = "numeric(10,1)")]
        public decimal XEquipement { get; set; } = 0;

        [Column("yequipement", TypeName = "numeric(10,1)")]
        public decimal YEquipement { get; set; } = 0;

        [Column("zequipement", TypeName = "numeric(10,1)")]
        public decimal ZEquipement { get; set; } = 0;

        [Column("estactif", TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeEquipement))]
        [InverseProperty(nameof(TypeEquipement.Equipements))]
        public virtual TypeEquipement? TypeEquipementNavigation { get => typeEquipementNavigation; set => typeEquipementNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdMur))]
        [InverseProperty(nameof(Mur.Equipements))]
        public virtual Mur? MurNavigation { get => murNavigation; set => murNavigation = value; }
    }
}
