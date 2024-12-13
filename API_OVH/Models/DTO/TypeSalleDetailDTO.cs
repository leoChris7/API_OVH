using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class TypeSalleDetailDTO
    {
        private ICollection<SalleSansNavigationDTO> salles = [];
        private int idTypeSalle;
        private string nomTypeSalle;

        [Required]
        public int IdTypeSalle { get => idTypeSalle; set => idTypeSalle = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }

        public ICollection<SalleSansNavigationDTO> Salles { get => salles; set => salles = value; }
    }
}
