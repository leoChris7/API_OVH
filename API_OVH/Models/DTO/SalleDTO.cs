using Humanizer;

namespace API_OVH.Models.DTO
{
    public class SalleDTO
    {
        private string nomsalle;
        private string nombatiment;
        private string direction;
        private int nbCapteurs;
        private int nbEquipements;
        private int nbMurs;

        public string Nomsalle { get => nomsalle; set => nomsalle = value; }
        public string Nombatiment { get => nombatiment; set => nombatiment = value; }
        public string Direction { get => direction; set => direction = value; }
        public int NbCapteurs { get => nbCapteurs; set => nbCapteurs = value; }
        public int NbEquipements { get => nbEquipements; set => nbEquipements = value; }
        public int NbMurs { get => nbMurs; set => nbMurs = value; }
    }
}
