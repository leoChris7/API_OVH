using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.DTO
{
    public class BatimentSansNavigationDTO
    {
        private int idBatiment;
        private string nomBatiment;

        [Required(ErrorMessage = "Id nécéssaire")]
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
    }
}
