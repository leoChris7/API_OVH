using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class CapteurDTO
    {
        private string nomCapteur;
        private string nomSalle;

        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }
        public string NomSalle { get => nomSalle; set => nomSalle = value; }
    }
}
