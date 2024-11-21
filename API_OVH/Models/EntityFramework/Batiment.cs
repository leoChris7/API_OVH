using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Batiment
    {
        private int idBatiment;
        private string? nomBatiment;
        private ICollection<Salle> salles;

        public int IdBatiment { get => idBatiment; set => idBatiment = value; }

        public string? NomBatiment { get => nomBatiment; set => nomBatiment = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Batiment.salles))]
        public ICollection<Salle> Salles { get => salles; set => salles = value; }
    }
}
