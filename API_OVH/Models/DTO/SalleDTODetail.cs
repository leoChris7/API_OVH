using API_OVH.Models.EntityFramework;
using Humanizer;
using System.Runtime.CompilerServices;

namespace API_OVH.Models.DTO
{
    public class SalleDTODetail
    {
        private int idSalle;
        private string nomSalle;
        private String nomBatiment;
        private String nomTypeSalle;
        private List<Capteur> capteurs;
        private List<Equipement> equipements;
        private List<Mur> murs;

        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public List<Mur> Murs { get => murs; set => murs = value; }
        public List<Equipement> Equipements { get => equipements; set => equipements = value; }
        public List<Capteur> Capteurs { get => capteurs; set => capteurs = value; }
        public string NomBatiment { get => nomBatiment; set => nomBatiment = value; }
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }
    }
}
