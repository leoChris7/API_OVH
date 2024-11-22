using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class UniteCapteur
    {
        private int idCapteur;
        private int idUnite;
        private Capteur? capteur;
        private Unite? unite;

        public int IdCapteur
        {
            get
            {
                return idCapteur;
            }

            set
            {
                idCapteur = value;
            }
        }

        public int IdUnite
        {
            get
            {
                return this.idUnite;
            }

            set
            {
                this.idUnite = value;
            }
        }

        [JsonIgnore]
        [ForeignKey(nameof(idUnite))]
        [InverseProperty(nameof(Unite.IdUnite))]
        public Unite? BatimentNavigation { get => batimentNavigation; set => batimentNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(idCapteur))]
        [InverseProperty(nameof(Capteur.Salles))]
        public Capteur? TypeSalleNavigation { get => typeSalleNavigation; set => typeSalleNavigation = value; }

    }
}
