namespace API_OVH.Models.EntityFramework
{
    public class Direction
    {
        private int iddirection;
        private string lettres_direction;

        public int Iddirection { get => iddirection; set => iddirection = value; }
        public string Lettres_direction { get => lettres_direction; set => lettres_direction = value; }
    }
}
