using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class CapteurDTO
    {
        private int idCapteur;
        private string nomCapteur;
        private string nomSalle;

        [Required]
        public int IdCapteur { get => idCapteur; set => idCapteur = value; }

        [Required]
        [MaxLength(25, ErrorMessage = "Le nom du capteur ne doit pas dépasser 25 caractères.")]
        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }

        [MaxLength(20, ErrorMessage = "Le nom de la salle ne doit pas dépasser 20 caractères.")]
        public string NomSalle { get => nomSalle; set => nomSalle = value; }

    }
}
