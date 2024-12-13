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

        [Required]
        public int IdMur { get => idMur; set => idMur = value; }

        [Range(1, 8, ErrorMessage = "Une cardinalité inconnue a été appliqué à un mur sans navigation dto (1 à 8)")]
        public short IdDirection { get => idDirection; set => idDirection = value; }

        public int IdSalle { get => idSalle; set => idSalle = value; }

        [Range(0, 99999999.99, ErrorMessage = "La longueur d'un mur doit être entre 0 et 99,999,999.99cm")]
        public decimal Longueur { get => longueur; set => longueur = value; }

        [Range(0, 99999.99, ErrorMessage = "La hauteur d'un mur doit être entre 0 et 99,999.99cm")]
        public decimal Hauteur { get => hauteur; set => hauteur = value; }

        [Range(-360, 360, ErrorMessage = "L'orientation doit être entre -360° et 360°")]
        public decimal Orientation { get => orientation; set => orientation = value; }
    }
}
