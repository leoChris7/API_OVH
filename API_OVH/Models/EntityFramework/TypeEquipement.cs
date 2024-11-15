namespace API_OVH.Models.EntityFramework
{
    public class TypeEquipement
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;

        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
    }
}
