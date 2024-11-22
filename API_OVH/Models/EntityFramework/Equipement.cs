using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Equipement
    {
        private int idEquipement;
        private int idSalle;
        private int idTypeEquipement;
        private string? nomEquipement;
        private double xEquipement;
        private double yEquipement;
        private double zEquipement;
        private TypeEquipement? typeEquipementNavigation;
        private Salle? salleNavigation;
        private ICollection<ValeurEquipement> valeursEquipements = new List<ValeurEquipement>();

        public int IdEquipement { get => idEquipement; set => idEquipement = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }
        public string? NomEquipement { get => nomEquipement; set => nomEquipement = value; }
        public double XEquipement { get => xEquipement; set => xEquipement = value; }
        public double YEquipement { get => yEquipement; set => yEquipement = value; }
        public double ZEquipement { get => zEquipement; set => zEquipement = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeEquipement))]
        [InverseProperty(nameof(TypeEquipement.Equipements))]
        public virtual TypeEquipement? TypeEquipementNavigation { get => typeEquipementNavigation; set => typeEquipementNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Equipements))]
        public virtual Salle? SalleNavigation { get => SalleNavigation; set => SalleNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(ValeurEquipement.EquipementNavigation))]
        public virtual ICollection<ValeurEquipement> ValeursEquipements { get => valeursEquipements; set => valeursEquipements = value; }
    }
}
