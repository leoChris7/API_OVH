using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public class Salle
    {
        private int idSalle;
        private int idBatiment;
        private int idtypeSalle;
        private string? nomSalle;
        private double superficieSalle;
        private Batiment? batimentNavigation;
        private TypeSalle? typeSalleNavigation;

        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }
        public int IdTypeSalle { get => idtypeSalle; set => idtypeSalle = value; }
        public string? NomSalle { get => nomSalle; set => nomSalle = value; }
        public double SuperficieSalle { get => superficieSalle; set => superficieSalle = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Salle.batimentNavigation))]
        public Batiment? BatimentNavigation { get => batimentNavigation; set => batimentNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Salle.typeSalleNavigation))]
        public TypeSalle? TypeSalleNavigation { get => typeSalleNavigation; set => typeSalleNavigation = value; }
    }
}
