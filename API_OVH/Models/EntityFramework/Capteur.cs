using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("capteur")]
    public partial class Capteur
    {
        private Mur? murNavigation;
        private ICollection<UniteCapteur> unitesCapteur = new List<UniteCapteur>();

        [Key]
        [Column("idcapteur")]
        public int IdCapteur { get; set; }

        [Column("idmur")]
        public int? IdMur { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Le nom ne doit pas dépasser 25 caractères.")]
        [Column("nomcapteur", TypeName = "varchar(25)")]
        public string NomCapteur { get; set; }

        [Length(3, 3, ErrorMessage = "L'état doit être de 3 caractères")]
        [EtatValidation(ErrorMessage = "Etat invalide")]
        [Column("estactif", TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("xcapteur", TypeName = "numeric(10,1)")]
        public decimal XCapteur { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("ycapteur", TypeName = "numeric(10,1)")]
        public decimal YCapteur { get; set; } = 0;

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        [Column("zcapteur", TypeName = "numeric(10,1)")]
        public decimal ZCapteur { get; set; } = 0;

        [JsonIgnore]
        [ForeignKey(nameof(IdMur))]
        [InverseProperty(nameof(Mur.Capteurs))]
        public Mur? MurNavigation { get => murNavigation; set => murNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(UniteCapteur.CapteurNavigation))]
        public virtual ICollection<UniteCapteur> UnitesCapteur { get => unitesCapteur; set => unitesCapteur = value; }
    }
}
