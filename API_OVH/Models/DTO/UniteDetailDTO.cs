using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class UniteDetailDTO
    {
        private int idUnite;
        private String sigleUnite;
        private String nomUnite;
        private List<CapteurSansNavigationDTO> capteurs;

        [Required]
        public int IdUnite { get => idUnite; set => idUnite = value; }

        [MaxLength(10, ErrorMessage = "Le sigle ne doit pas dépasser 10 caractères.")]
        public string SigleUnite { get => sigleUnite; set => sigleUnite = value; }

        [MaxLength(50, ErrorMessage = "Le nom de l'unité ne doit pas dépasser 50 caractères.")]
        [Required]
        public string NomUnite { get => nomUnite; set => nomUnite = value; }

        public List<CapteurSansNavigationDTO> Capteurs { get => capteurs; set => capteurs = value; }
    }
}
