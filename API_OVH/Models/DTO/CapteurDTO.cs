using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class CapteurDTO
    {
        private int idCapteur;
        private string nomCapteur;
        private string nomSalle;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }
        public string NomSalle { get => nomSalle; set => nomSalle = value; }

    }
}
