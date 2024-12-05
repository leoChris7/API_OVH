using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class EquipementSansNavigation
    {
        public int IdEquipement { get; set; }

        public int IdMur { get; set; }

        public int IdTypeEquipement { get; set; }

        public string NomEquipement { get; set; }

        public decimal Longueur { get; set; }

        public decimal Largeur { get; set; }

        public decimal Hauteur { get; set; }

        public decimal XEquipement { get; set; }

        public decimal YEquipement { get; set; }

        public decimal ZEquipement { get; set; }

        public string EstActif { get; set; }
    }
}
