using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class TypeSalleDetailDTO
    {
        private ICollection<SalleSansNavigationDTO> salles = new List<SalleSansNavigationDTO>();
        private int idTypeSalle;
        private string nomTypeSalle;

        public ICollection<SalleSansNavigationDTO> Salles { get => salles; set => salles = value; }
        public int IdTypeSalle { get => idTypeSalle; set => idTypeSalle = value; }
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }
    }
}
