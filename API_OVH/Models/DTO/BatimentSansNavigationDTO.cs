using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.DTO
{
    public class BatimentSansNavigationDTO
    {
        private int idBatiment;
        private string nomBatiment;

        public int IdBatiment { get => idBatiment; set => idBatiment = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
    }
}
