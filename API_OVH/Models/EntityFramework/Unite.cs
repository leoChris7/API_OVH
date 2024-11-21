using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Unite
    {
        private int idUnite;
        private string nomUnite;
        private string? sigleUnite;
        private ICollection<ValeurEquipement> valeursEquipements = new List<ValeurEquipement>();

        public int IdUnite { get => idUnite; set => idUnite = value; }
        public string NomUnite { get => nomUnite; set => nomUnite = value; }
        public string? SigleUnite { get => sigleUnite; set => sigleUnite = value; }

        [JsonIgnore]
        [InverseProperty(nameof(ValeurEquipement.UniteNavigation))]
        public virtual ICollection<ValeurEquipement> ValeursEquipements { get => valeursEquipements; set => valeursEquipements = value; }
    }
}
