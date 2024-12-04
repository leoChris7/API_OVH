using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("CAPTEUR")]
    public partial class Capteur
    {
        private Mur? murNavigation;
        private ICollection<UniteCapteur> unitesCapteur = new List<UniteCapteur>();

        [Key]
        [Column("IDCAPTEUR")]
        public int IdCapteur { get; set; }

        [Column("IDMUR")]
        public int? IdMur { get; set; }

        [Required]
        [Column("NOMCAPTEUR", TypeName = "varchar(25)")]
        public string NomCapteur { get; set; }

        [Column("ESTACTIF", TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [Column("XCAPTEUR", TypeName = "numeric(10,1)")]
        public decimal XCapteur { get; set; } = 0;

        [Column("YCAPTEUR", TypeName = "numeric(10,1)")]
        public decimal YCapteur { get; set; } = 0;

        [Column("ZCAPTEUR", TypeName = "numeric(10,1)")]
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
