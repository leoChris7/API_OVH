using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("salle")]
    public partial class Salle
    {
        private Batiment? batimentNavigation;
        private TypeSalle? typeSalleNavigation;
        private ICollection<Capteur> capteurs = new List<Capteur>();
        private ICollection<Equipement> equipements = new List<Equipement>();
        private ICollection<Mur> murs = new List<Mur>();

        [Key]
        [Column("idsalle")]
        public int IdSalle { get; set; }

        [Column("idbatiment")]
        public int IdBatiment { get; set; }

        [Column("idtypesalle")]
        public int IdTypeSalle { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        [Column("nomsalle", TypeName = "varchar(20)")]
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
