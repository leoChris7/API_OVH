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

        public ICollection<MurSansNavigationDTO> Murs { get => murs; set => murs = value; }
        public short IdDirection { get => idDirection; set => idDirection = value; }
        public string LettresDirection { get => lettresDirection; set => lettresDirection = value; }
    }
}
