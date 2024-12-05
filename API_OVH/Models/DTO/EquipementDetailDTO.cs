using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class EquipementDetailDTO
    {
        private int idEquipement;
        private String nomEquipement;
        private Salle salle;
        private String nomTypeEquipement;
        private String dimensions; // L*l*H
        private String estActif; 

        private decimal positionX;
        private decimal positionY;
        private decimal positionZ;

        public string NomEquipement { get => nomEquipement; set => nomEquipement = value; }
        public Salle Salle { get => salle; set => salle = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
        public string Dimensions { get => dimensions; set => dimensions = value; }
        public string EstActif { get => estActif; set => estActif = value; }
        public decimal PositionX { get => positionX; set => positionX = value; }
        public decimal PositionY { get => positionY; set => positionY = value; }
        public decimal PositionZ { get => positionZ; set => positionZ = value; }
        public int IdEquipement { get => idEquipement; set => idEquipement = value; }
    }
}
