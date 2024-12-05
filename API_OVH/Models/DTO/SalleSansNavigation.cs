using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class SalleSansNavigation
    {
        public int IdSalle { get; set; }

        public int IdBatiment { get; set; }

        public int IdTypeSalle { get; set; }

        public string NomSalle { get; set; }
    }
}
