using API_OVH.Models.EntityFramework;
using Humanizer;

namespace API_OVH.Models.DTO
{
    public class SalleDTODetail
    {
        private int idSalle;
        private string nomSalle;
        private string nomBatiment;
        private List<Capteur> capteurs;
        private List<Equipement> equipements;
        private List<Mur> murs;

        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public List<Mur> Murs { get => murs; set => murs = value; }
        public List<Equipement> Equipements { get => equipements; set => equipements = value; }
        public List<Capteur> Capteurs { get => capteurs; set => capteurs = value; }
    }
}
