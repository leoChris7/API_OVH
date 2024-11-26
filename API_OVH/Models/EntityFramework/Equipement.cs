using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Equipement
    {
        private TypeEquipement typeEquipementNavigation;
        private Salle salleNavigation;
        private ICollection<ValeurEquipement> valeursEquipements = new List<ValeurEquipement>();

        [Key]
        public int IdEquipement { get; set; }

        [Required]
        public int IdSalle { get; set; }

        [Required]
        public int IdTypeEquipement { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string NomEquipement { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Longueur { get; set; } = 0;

        [Column(TypeName = "numeric")]
        public decimal Largeur { get; set; } = 0;

        [Column(TypeName = "numeric")]
        public decimal Hauteur { get; set; } = 0;

        [Column(TypeName = "numeric(10,1)")]
        public decimal XEquipement { get; set; } = 0;

        [Column(TypeName = "numeric(10,1)")]
        public decimal YEquipement { get; set; } = 0;

        [Column(TypeName = "numeric(10,1)")]
        public decimal ZEquipement { get; set; } = 0;

        [Column(TypeName = "char(3)")]
        public string EstActif { get; set; } = "NSP";

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeEquipement))]
        [InverseProperty(nameof(TypeEquipement.Equipements))]
        public virtual TypeEquipement? TypeEquipementNavigation { get => typeEquipementNavigation; set => typeEquipementNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Equipements))]
        public virtual TypeEquipement? SalleNavigation { get => typeEquipementNavigation; set => typeEquipementNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(ValeurEquipement.EquipementNavigation))]
        public virtual ICollection<ValeurEquipement> ValeursEquipements { get => valeursEquipements; set => valeursEquipements = value; }
    }
}
