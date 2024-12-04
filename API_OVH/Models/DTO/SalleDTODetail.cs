using Humanizer;

namespace API_OVH.Models.DTO
{
    public class SalleDTODetail
    {
        private string nomSalle;
        private string nomBatiment;
        private int nbCapteurs;
        private int nbEquipements;
        private int nbMurs;

        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
        public int NbCapteurs { get => nbCapteurs; set => nbCapteurs = value; }
        public int NbEquipements { get => nbEquipements; set => nbEquipements = value; }
        public int NbMurs { get => nbMurs; set => nbMurs = value; }
    }
}
