using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class UniteDetailDTO
    {
        private int idUnite;
        private String sigleUnite;
        private String nomUnite;
        private List<CapteurSansNavigationDTO> capteurs;

        public int IdUnite { get => idUnite; set => idUnite = value; }
        public string SigleUnite { get => sigleUnite; set => sigleUnite = value; }
        public string NomUnite { get => nomUnite; set => nomUnite = value; }
        public List<CapteurSansNavigationDTO> Capteurs { get => capteurs; set => capteurs = value; }
    }
}
