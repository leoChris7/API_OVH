using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class TypeEquipementDTO
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;

        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
    }
}
