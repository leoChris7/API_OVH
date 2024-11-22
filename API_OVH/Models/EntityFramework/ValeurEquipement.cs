using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class ValeurEquipement
    {
        private int idCaracteristique;
        private int idEquipement;
        private int idUnite;
        private double valeur;
        private CaracteristiqueEquipement caracteristiqueEquipementNavigation;
        private Equipement equipementNavigation;
        private Unite uniteNavigation;

        public int IdCaracteristique { get => idCaracteristique; set => idCaracteristique = value; }
        public int IdEquipement { get => idEquipement; set => idEquipement = value; }
        public int IdUnite { get => idUnite; set => idUnite = value; }
        public double Valeur { get => valeur; set => valeur = value; }
        
        [JsonIgnore]
        [ForeignKey(nameof(IdCaracteristique))]
        [InverseProperty(nameof(CaracteristiqueEquipement.ValeursEquipements))]
        public virtual CaracteristiqueEquipement? CaracteristiqueEquipementNavigation { get => caracteristiqueEquipementNavigation; set => caracteristiqueEquipementNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdEquipement))]
        [InverseProperty(nameof(Equipement.ValeursEquipements))]
        public virtual Equipement? EquipementNavigation { get => equipementNavigation; set => equipementNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdUnite))]
        [InverseProperty(nameof(Unite.ValeursEquipements))]
        public virtual Unite? UniteNavigation { get => uniteNavigation; set => uniteNavigation = value; }
    }
}
