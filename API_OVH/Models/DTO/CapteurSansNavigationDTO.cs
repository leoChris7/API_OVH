using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class CapteurSansNavigationDTO
    {
        public int IdCapteur { get; set; }

        public int? IdMur { get; set; }

        public string NomCapteur { get; set; }

        public string EstActif { get; set; }

        public decimal XCapteur { get; set; }

        public decimal YCapteur { get; set; }

        public decimal ZCapteur { get; set; }
    }
}
