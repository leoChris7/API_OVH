using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class MurDTO
    {
        private int idMur;
        private string direction;
        private string nomSalle;
        private decimal orientation;

        [Required]
        public int IdMur { get => idMur; set => idMur = value; }

        [Length(1, 2, ErrorMessage = "La cardinalité doit être de 1 (N) ou 2 caractère (NO)")]
        [DirectionValidation(ErrorMessage = "La direction doit être une cardinalité (N, S, E, O, NO, NE, SO, SE).")]
        public string Direction { get => direction; set => direction = value; }

        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomSalle { get => nomSalle; set => nomSalle = value; }

        [Range(-360, 360, ErrorMessage = "L'orientation doit être entre -360° et 360°")]
        public decimal Orientation { get => orientation; set => orientation = value; }

    }
}
