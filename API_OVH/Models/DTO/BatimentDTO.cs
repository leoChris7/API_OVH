using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class BatimentDTO
    {
        private int idBatiment;
        private string nomBatiment;
        private int nbSalle;

        [Required]
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }

        [Range(0, 2147483647, ErrorMessage = "Le nombre de salle ne doit pas dépasser 2,147,483,647 unités.")]
        public int NbSalle { get => nbSalle; set => nbSalle = value; }
    }
}
