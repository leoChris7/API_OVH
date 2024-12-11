using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [PrimaryKey(nameof(IdCapteur), nameof(IdUnite))]
    [Table("unite_capteur")]
    public partial class UniteCapteur
    {
        private Capteur capteurNavigation;
        private Unite uniteNavigation;

        [Key]
        [Column("idcapteur")]
        public int IdCapteur { get; set; }

        [Key]
        [Column("idunite")]
        public int IdUnite { get; set; }

        [ForeignKey(nameof(IdCapteur))]
        [InverseProperty(nameof(Capteur.UnitesCapteur))]
        public virtual Capteur CapteurNavigation { get => capteurNavigation; set => capteurNavigation = value; }

        [ForeignKey(nameof(IdUnite))]
        [InverseProperty(nameof(Unite.UnitesCapteur))]
        public virtual Unite UniteNavigation { get => uniteNavigation; set => uniteNavigation = value; }
    }
}
