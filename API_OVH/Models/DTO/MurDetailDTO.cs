using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class MurDetailDTO
    {
        private SalleSansNavigationDTO? salleNavigation;
        private DirectionSansNavigationDTO? directionNavigation;
        private ICollection<CapteurSansNavigationDTO> capteurs = [];
        private ICollection<EquipementSansNavigationDTO> equipements = [];

        [Required]
        public int IdMur { get; set; }

        [Range(1,8, ErrorMessage = "Une cardinalité inconnue a été appliqué à un mur detail dto (1 à 8)")]
        public short IdDirection { get; set; }

        public int IdSalle { get; set; }

        [Range(0, 99999999.99, ErrorMessage = "La longueur d'un mur doit être entre 0 et 99,999,999.99cm")]
        public decimal Longueur { get; set; } = 0;

        [Range(0, 99999.99, ErrorMessage = "La hauteur d'un mur doit être entre 0 et 99,999.99cm")]
        public decimal Hauteur { get; set; } = 0;

        [Range(-360, 360, ErrorMessage = "L'orientation doit être entre -360° et 360°")]
        public decimal Orientation { get; set; } = 0;

        public SalleSansNavigationDTO? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }
        public DirectionSansNavigationDTO? DirectionNavigation { get => directionNavigation; set => directionNavigation = value; }
        public virtual ICollection<CapteurSansNavigationDTO> Capteurs { get => capteurs; set => capteurs = value; }
        public virtual ICollection<EquipementSansNavigationDTO> Equipements { get => equipements; set => equipements = value; }
    }
}
