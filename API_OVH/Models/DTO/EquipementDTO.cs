using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class EquipementDTO
    {
        private string nomequipement;
        private string dimensions; // Dimensions: LxlxH
        private string nomSalleEquipement;
        private string nomTypeEquipement;
        private String estActif;

        public string Nomequipement { get => nomequipement; set => nomequipement = value; }
        public string Dimensions { get => dimensions; set => dimensions = value; }
        public string NomSalleEquipement { get => nomSalleEquipement; set => nomSalleEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
        public string EstActif { get => estActif; set => estActif = value; }
    }
}
