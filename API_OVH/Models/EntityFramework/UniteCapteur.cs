using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [PrimaryKey(nameof(IdCapteur), nameof(IdUnite))]
    [Table("UNITE_CAPTEUR")]
    public partial class UniteCapteur
    {
        private Capteur capteurNavigation;
        private Unite uniteNavigation;

        [Key]
        [Column("IDCAPTEUR")]
        public int IdCapteur { get; set; }

        [Key]
        [Column("IDUNITE")]
        public int IdUnite { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdCapteur))]
        [InverseProperty(nameof(Capteur.UnitesCapteur))]
        public virtual Capteur CapteurNavigation { get => capteurNavigation; set => capteurNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdUnite))]
        [InverseProperty(nameof(Unite.UnitesCapteur))]
        public virtual Unite UniteNavigation { get => uniteNavigation; set => uniteNavigation = value; }
    }
}
