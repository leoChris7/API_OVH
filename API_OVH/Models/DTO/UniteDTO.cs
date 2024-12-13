using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class UniteDTO
    {
        private int idUnite;
        private String sigleUnite;
        private String nomUnite;

        [Required]
        public int IdUnite { get => idUnite; set => idUnite = value; }

        [MaxLength(10, ErrorMessage = "Le sigle ne doit pas dépasser 10 caractères.")]
        public string SigleUnite { get => sigleUnite; set => sigleUnite = value; }

        [Required]
        [MaxLength(50, ErrorMessage = "Le nom de l'unité ne doit pas dépasser 50 caractères.")]
        public string NomUnite { get => nomUnite; set => nomUnite = value; }
    }
}
