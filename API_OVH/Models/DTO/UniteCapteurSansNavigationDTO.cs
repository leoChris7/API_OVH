using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class UniteCapteurSansNavigationDTO
    {
        private int idCapteur;
        private int idUnite;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public int IdUnite { get => idUnite; set => idUnite = value; }
    }
}
