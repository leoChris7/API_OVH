namespace API_OVH.Models.DTO
{
    public class DirectionSansNavigationDTO
    {
        private short idDirection;
        private string lettresDirection;

        public short IdDirection { get => idDirection; set => idDirection = value; }
        public string LettresDirection { get => lettresDirection; set => lettresDirection = value; }
    }
}
