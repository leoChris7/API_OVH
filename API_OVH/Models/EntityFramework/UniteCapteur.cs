using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class UniteCapteur
    {
        private int idCapteur;
        private int idUnite;
        private Capteur capteurNavigation;
        private Unite uniteNavigation;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public int IdUnite { get => idUnite; set => idUnite = value; }

        [JsonIgnore]
        [ForeignKey(nameof(idCapteur))]
        [InverseProperty(nameof(Capteur.UnitesCapteur))]
        public virtual Capteur CapteurNavigation { get => capteurNavigation; set => capteurNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdUnite))]
        [InverseProperty(nameof(Unite.UnitesCapteur))]
        public virtual Unite UniteNavigation { get => uniteNavigation; set => uniteNavigation = value; }
    }
}
