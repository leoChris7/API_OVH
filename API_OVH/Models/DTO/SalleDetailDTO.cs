using API_OVH.Models.EntityFramework;
using Humanizer;
using System.Runtime.CompilerServices;

namespace API_OVH.Models.DTO
{
    public class SalleDetailDTO
    {
        private int idSalle;
        private string nomSalle;
        private TypeSalleDTO typeSalle;
        private BatimentSansNavigationDTO batiment;
        private List<CapteurSansNavigationDTO> capteurs;
        private List<EquipementSansNavigationDTO> equipements;
        private List<MurSansNavigationDTO> murs;

        public string NomSalle { get => nomSalle; set => nomSalle = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public TypeSalleDTO TypeSalle { get => typeSalle; set => typeSalle = value; }
        public BatimentSansNavigationDTO Batiment { get => batiment; set => batiment = value; }
        public List<CapteurSansNavigationDTO> Capteurs { get => capteurs; set => capteurs = value; }
        public List<EquipementSansNavigationDTO> Equipements { get => equipements; set => equipements = value; }
        public List<MurSansNavigationDTO> Murs { get => murs; set => murs = value; }
    }
}
