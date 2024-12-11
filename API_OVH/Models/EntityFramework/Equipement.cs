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
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        [Column("nomequipement", TypeName = "varchar(20)")]
        public string NomEquipement { get; set; }

        [Range(0.0, 999999999.9, ErrorMessage = "La longueur doit être comprise entre 0 et 999,999,999.9cm.")]
        [Column("longueur", TypeName = "numeric(10,1)")]
        public decimal Longueur { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "La largeur doit être comprise entre 0 et 999,999,999.9cm.")]
        [Column("largeur", TypeName = "numeric(10,1)")]
        public decimal Largeur { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "La hauteur doit être comprise entre 0 et 999,999,999.9cm.")]
        [Column("hauteur", TypeName = "numeric(10,1)")]
        public decimal Hauteur { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("xequipement", TypeName = "numeric(10,1)")]
        public decimal XEquipement { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("yequipement", TypeName = "numeric(10,1)")]
        public decimal YEquipement { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("zequipement", TypeName = "numeric(10,1)")]
        public decimal ZEquipement { get; set; } = 0;

        [EtatValidationAttribute(ErrorMessage = "L'état doit être à OUI, NON ou NSP")]
        [Length(3, 3, ErrorMessage = "L'état doit être de 3 lettres")]
        [Column("estactif", TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [ForeignKey(nameof(IdTypeEquipement))]
        [InverseProperty(nameof(TypeEquipement.Equipements))]
        public virtual TypeEquipement? TypeEquipementNavigation { get => typeEquipementNavigation; set => typeEquipementNavigation = value; }

        [ForeignKey(nameof(IdMur))]
        [InverseProperty(nameof(Mur.Equipements))]
        public virtual Mur? MurNavigation { get => murNavigation; set => murNavigation = value; }
    }
}
