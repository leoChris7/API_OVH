using Humanizer;

namespace API_OVH.Models.DTO
{
    public class SalleDTO
    {
        private string nomSalle;
        private string nomBatiment;
        private string nomType;

        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
        public string NomType { get => nomType; set => nomType = value; }
    }
}
