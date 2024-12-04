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
        [Column("nomcapteur", TypeName = "varchar(25)")]
        public string NomCapteur { get; set; }

        [Column("estactif", TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [Column("xcapteur", TypeName = "numeric(10,1)")]
        public decimal XCapteur { get; set; } = 0;

        [Column("ycapteur", TypeName = "numeric(10,1)")]
        public decimal YCapteur { get; set; } = 0;

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
