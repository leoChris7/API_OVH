using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class SalleDTO
    {
        private int idSalle;
        private string nomSalle;
        private string nomBatiment;
        private string nomType;

        [Required]
        public int IdSalle { get => idSalle; set => idSalle = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom de la salle ne doit pas dépasser 20 caractères.")]
        public string NomSalle { get => nomSalle; set => nomSalle = value; }

        [MaxLength(20, ErrorMessage = "Le nom du batiment ne doit pas dépasser 20 caractères.")]
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }

        [MaxLength(20, ErrorMessage = "Le nom de type de salle ne doit pas dépasser 20 caractères.")]
        public string NomType { get => nomType; set => nomType = value; }
    }
}
