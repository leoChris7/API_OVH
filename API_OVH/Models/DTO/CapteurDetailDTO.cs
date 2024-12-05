using API_OVH.Models.EntityFramework;

namespace API_OVH.Models.DTO
{
    public class CapteurDetailDTO
    {
        private int idCapteur;
        private string nomCapteur;
        private string nomSalle;
        private List<Unite> unites;
        private Mur mur;

        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }
        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public List<Unite> Unites { get => unites; set => unites = value; }
        public Mur Mur { get => mur; set => mur = value; }
        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
    }
}
