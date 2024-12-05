using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("mur")]
    public partial class Mur
    {
        private Salle? salleNavigation;
        private Direction? directionNavigation;
        private ICollection<Capteur> capteurs = new List<Capteur>();
        private ICollection<Equipement> equipements = new List<Equipement>();

        [Key]
        [Column("idmur")]
        public int IdMur { get; set; }

        [Column("iddirection")]
        public short IdDirection { get; set; }

        [Column("idsalle")]
        public int IdSalle { get; set; }

        [Column("longueur", TypeName = "numeric")]
        public decimal Longueur { get; set; } = 0;

        [Column("hauteur", TypeName = "numeric(4,2)")]
        public decimal Hauteur { get; set; } = 0;

        [Column("orientation", TypeName = "numeric(9,6)")]
        public decimal Orientation { get; set; } = 0;

        [JsonIgnore]
        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Murs))]
        public Salle? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdDirection))]
        [InverseProperty(nameof(Direction.Murs))]
        public Direction? DirectionNavigation { get => directionNavigation; set => directionNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Capteur.MurNavigation))]
        public virtual ICollection<Capteur> Capteurs { get => capteurs; set => capteurs = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Equipement.MurNavigation))]
        public virtual ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }
    }
}
