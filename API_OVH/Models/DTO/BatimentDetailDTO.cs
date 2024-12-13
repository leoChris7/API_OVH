using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class BatimentDetailDTO
    {
        private ICollection<SalleSansNavigationDTO> salles;
        private int idBatiment;
        private string nomBatiment;

        [Required]
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }

        public ICollection<SalleSansNavigationDTO> Salles { get => salles; set => salles = value; }
    }
}
