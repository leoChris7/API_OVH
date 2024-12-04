using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("SALLE")]
    public partial class Salle
    {
        private Batiment? batimentNavigation;
        private TypeSalle? typeSalleNavigation;
        private ICollection<Capteur> capteurs = new List<Capteur>();
        private ICollection<Equipement> equipements = new List<Equipement>();
        private ICollection<Mur> murs = new List<Mur>();

        [Key]
        [Column("IDSALLE")]
        public int IdSalle { get; set; }

        [Column("IDBATIMENT")]
        public int IdBatiment { get; set; }

        [Column("IDTYPESALLE")]
        public int IdTypeSalle { get; set; }

        [Required]
        [Column("NOMSALLE", TypeName = "varchar(20)")]
        public string NomSalle { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdBatiment))]
        [InverseProperty(nameof(Batiment.Salles))]
        public Batiment? BatimentNavigation { get => batimentNavigation; set => batimentNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeSalle))]
        [InverseProperty(nameof(TypeSalle.Salles))]
        public TypeSalle? TypeSalleNavigation { get => typeSalleNavigation; set => typeSalleNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Mur.SalleNavigation))]
        public virtual ICollection<Mur> Murs { get => murs; set => murs = value; }
    }
}
