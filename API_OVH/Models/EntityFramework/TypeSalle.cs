using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class TypeSalle
    {
        private int idTypeSalle;
        private string nomTypeSalle;
        private ICollection<Salle> salles = new List<Salle>();

        public int IdTypeSalle { get => idTypeSalle; set => idTypeSalle = value; }
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Salle.TypeSalleNavigation))]
        public ICollection<Salle> Salles { get => salles; set => salles = value; }
    }
}
