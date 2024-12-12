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

        public ICollection<SalleSansNavigationDTO> Salles { get => salles; set => salles = value; }
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
    }
}
