using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class MurSansNavigationDTO
    {
        private int idMur;
        private short idDirection;
        private int idSalle;
        private decimal longueur;
        private decimal hauteur;
        private decimal orientation;

        public int IdMur { get => idMur; set => idMur = value; }
        public short IdDirection { get => idDirection; set => idDirection = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public decimal Longueur { get => longueur; set => longueur = value; }
        public decimal Hauteur { get => hauteur; set => hauteur = value; }
        public decimal Orientation { get => orientation; set => orientation = value; }
    }
}
