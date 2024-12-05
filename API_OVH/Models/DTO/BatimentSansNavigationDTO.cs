using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.DTO
{
    public class BatimentSansNavigationDTO
    {
        public int IdBatiment { get; set; }

        public string NomBatiment { get; set; }
    }
}
