using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class TypeEquipementDTO
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;
        private string nomSalle;
        private List<Equipement> equipements;

        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
        public List<Equipement> Equipements { get => equipements; set => equipements = value; }
        public string NomSalle { get => nomSalle; set => nomSalle = value; }
    }
}
