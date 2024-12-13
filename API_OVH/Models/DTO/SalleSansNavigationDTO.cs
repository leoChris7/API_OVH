using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class SalleSansNavigationDTO
    {
        [Required]
        public int IdSalle { get; set; }

        public int IdBatiment { get; set; }

        public int IdTypeSalle { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom de la salle ne doit pas dépasser 20 caractères.")]
        public string NomSalle { get; set; }
    }
}
