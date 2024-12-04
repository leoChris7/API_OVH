using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("EQUIPEMENT")]
    public partial class Equipement
    {
        private TypeEquipement typeEquipementNavigation;
        private Mur murNavigation;

        [Key]
        [Column("IDEQUIPEMENT")] 
        public int IdEquipement { get; set; }

        [Column("IDMUR")]
        public int IdMur { get; set; }

        [Column("IDTYPEEQUIPEMENT")]
        public int IdTypeEquipement { get; set; }

        [Required]
        [Column("NOMEQUIPEMENT", TypeName = "varchar(20)")]
        public string NomEquipement { get; set; }

        [Column("LONGUEUR", TypeName = "numeric")]
        public decimal Longueur { get; set; } = 0;

        [Column("LARGEUR", TypeName = "numeric")]
        public decimal Largeur { get; set; } = 0;

        [Column("HAUTEUR", TypeName = "numeric")]
        public decimal Hauteur { get; set; } = 0;

        [Column("XEQUIPEMENT", TypeName = "numeric(10,1)")]
        public decimal XEquipement { get; set; } = 0;

        [Column("YEQUIPEMENT", TypeName = "numeric(10,1)")]
        public decimal YEquipement { get; set; } = 0;

        [Column("ZEQUIPEMENT", TypeName = "numeric(10,1)")]
        public decimal ZEquipement { get; set; } = 0;

        [Column("ESTACTIF", TypeName = "char(3)")]
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
