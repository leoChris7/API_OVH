using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class EquipementDetailDTO
    {
        private int idEquipement;
        private String nomEquipement;
        private String dimensions; // L*l*H
        private String estActif;
        private SalleSansNavigationDTO salle;
        private TypeEquipementDTO typeEquipement;

        private decimal positionX;
        private decimal positionY;
        private decimal positionZ;

        public int IdEquipement { get => idEquipement; set => idEquipement = value; }
        public string NomEquipement { get => nomEquipement; set => nomEquipement = value; }
        public string Dimensions { get => dimensions; set => dimensions = value; }
        public string EstActif { get => estActif; set => estActif = value; }
        public decimal PositionX { get => positionX; set => positionX = value; }
        public decimal PositionY { get => positionY; set => positionY = value; }
        public decimal PositionZ { get => positionZ; set => positionZ = value; }
        public SalleSansNavigationDTO Salle { get => salle; set => salle = value; }
        public TypeEquipementDTO TypeEquipement { get => typeEquipement; set => typeEquipement = value; }
    }
}
