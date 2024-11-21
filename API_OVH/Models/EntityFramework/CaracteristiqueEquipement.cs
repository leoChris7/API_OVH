using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public class CaracteristiqueEquipement
    {
        private int idCaracteristique;
        private string nomCaracteristique;
        private ICollection<ValeurEquipement> valeursEquipements = new List<ValeurEquipement>();

        public int IdCaracteristique { get => idCaracteristique; set => idCaracteristique = value; }
        public string NomCaracteristique { get => nomCaracteristique; set => nomCaracteristique = value; }

        [JsonIgnore]
        [InverseProperty(nameof(ValeurEquipement.CaracteristiqueEquipementNavigation))]
        public virtual ICollection<ValeurEquipement> ValeursEquipements { get => valeursEquipements; set => valeursEquipements = value; }
    }
}
