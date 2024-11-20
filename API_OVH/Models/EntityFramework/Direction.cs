namespace API_OVH.Models.EntityFramework
{
    public class Direction
    {
        private int idDirection;
        private string lettresDirection;

        public int IdDirection { get => idDirection; set => idDirection = value; }
        public string LettresDirection { get => lettresDirection; set => lettresDirection = value; }
    }
}
