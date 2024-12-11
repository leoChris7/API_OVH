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

        [Range(1, 8, ErrorMessage = "L'ID de la direction doit être entre 1 et 8 (N, S, E, O, NO, NE, SE, SO)")]
        [Column("iddirection")]
        public short IdDirection { get; set; }

        [Column("idsalle")]
        public int IdSalle { get; set; }

        [Range(0, 99999999.99, ErrorMessage = "La longueur d'un mur doit être entre 0 et 99,999,999.99cm")]
        [Column("longueur", TypeName = "numeric(10,2)")]
        public decimal Longueur { get; set; } = 0;

        [Range(0, 99999.99, ErrorMessage = "La hauteur d'un mur doit être entre 0 et 99,999.99cm")]
        [Column("hauteur", TypeName = "numeric(7,2)")]
        public decimal Hauteur { get; set; } = 0;

        [Range(-360, 360, ErrorMessage = "L'orientation doit être entre -360° et 360°")]
        [Column("orientation", TypeName = "numeric(9,6)")]
        public decimal Orientation { get; set; } = 0;

        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Murs))]
        public Salle? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }

        [ForeignKey(nameof(IdDirection))]
        [InverseProperty(nameof(Direction.Murs))]
        public Direction? DirectionNavigation { get => directionNavigation; set => directionNavigation = value; }

        [InverseProperty(nameof(Capteur.MurNavigation))]
        public virtual ICollection<Capteur> Capteurs { get => capteurs; set => capteurs = value; }

        [InverseProperty(nameof(Equipement.MurNavigation))]
        public virtual ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }
    }
}
