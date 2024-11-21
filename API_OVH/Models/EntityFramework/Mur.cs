using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Mur
    {
        private int idMur;
        private int idDirection;
        private int idSalle;
        private double longueur;
        private double hauteur;
        private float orientation;
        private Salle? salleNavigation;
        private Direction? directionNavigation;

        public int IdMur { get => idMur; set => idMur = value; }
        public int IdDirection { get => idDirection; set => idDirection = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public double Longueur { get => longueur; set => longueur = value; }
        public double Hauteur { get => hauteur; set => hauteur = value; }
        public float Orientation { get => orientation; set => orientation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Murs))]
        public Salle? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdDirection))]
        [InverseProperty(nameof(Direction.Murs))]
        public Direction? DirectionNavigation { get => directionNavigation; set => directionNavigation = value; }
    }
}
