using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class TypeMesure
    {
        private int idTypeMesure;
        private string nomTypeMesure;
        private string sigleTypeMesure;
        private ICollection<Capteur> capteurs = new List<Capteur>();

        public int IdTypeMesure { get => idTypeMesure; set => idTypeMesure = value; }
        public string NomTypeMesure { get => nomTypeMesure; set => nomTypeMesure = value; }
        public string SigleTypeMesure { get => sigleTypeMesure; set => sigleTypeMesure = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Capteur.TypeMesureNavigation))]
        public ICollection<Capteur> Capteurs { get => capteurs; set => capteurs = value; }
    }
}
