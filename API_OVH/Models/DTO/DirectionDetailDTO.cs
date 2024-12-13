using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class DirectionDetailDTO
    {
        private ICollection<MurSansNavigationDTO> murs;
        private short idDirection;
        private string lettresDirection;

        [Required]
        public short IdDirection { get => idDirection; set => idDirection = value; }
        
        [Required]
        [Length(1, 2, ErrorMessage = "La cardinalité doit être de 1 (N) ou 2 caractère (NO)")]
        [DirectionValidation(ErrorMessage = "La direction doit être une cardinalité (N, S, E, O, NO, NE, SO, SE).")]
        public string LettresDirection { get => lettresDirection; set => lettresDirection = value; }

        public ICollection<MurSansNavigationDTO> Murs { get => murs; set => murs = value; }
    }
}
