using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class MurDetailDTO
    {
        private SalleSansNavigationDTO? salleNavigation;
        private DirectionSansNavigationDTO? directionNavigation;
        private ICollection<CapteurSansNavigationDTO> capteurs = new List<CapteurSansNavigationDTO>();
        private ICollection<EquipementSansNavigationDTO> equipements = new List<EquipementSansNavigationDTO>();

        public int IdMur { get; set; }
        public short IdDirection { get; set; }
        public int IdSalle { get; set; }
        public decimal Longueur { get; set; } = 0;
        public decimal Hauteur { get; set; } = 0;
        public decimal Orientation { get; set; } = 0;
        public SalleSansNavigationDTO? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }
        public DirectionSansNavigationDTO? DirectionNavigation { get => directionNavigation; set => directionNavigation = value; }
        public virtual ICollection<CapteurSansNavigationDTO> Capteurs { get => capteurs; set => capteurs = value; }
        public virtual ICollection<EquipementSansNavigationDTO> Equipements { get => equipements; set => equipements = value; }
    }
}
