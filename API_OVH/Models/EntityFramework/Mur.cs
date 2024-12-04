using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("MUR")]
    public partial class Mur
    {
        private Salle? salleNavigation;
        private Direction? directionNavigation;
        private ICollection<Capteur> capteurs = new List<Capteur>();
        private ICollection<Equipement> equipements = new List<Equipement>();

        [Key]
        [Column("IDMUR")]
        public int IdMur { get; set; }

        [Column("IDDIRECTION")]
        public short IdDirection { get; set; }

        [Column("IDSALLE")]
        public int IdSalle { get; set; }

        [Column("LONGUEUR", TypeName = "numeric")]
        public decimal Longueur { get; set; } = 0;

        [Column("HAUTEUR", TypeName = "numeric(4,2)")]
        public decimal Hauteur { get; set; } = 0;

        [Column("ORIENTATION", TypeName = "numeric(8,6)")]
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
