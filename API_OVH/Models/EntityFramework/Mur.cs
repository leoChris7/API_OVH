namespace API_OVH.Models.EntityFramework
{
    public class Mur
    {
        private int idMur;
        private int idDirection;
        private int idSalle;
        private double longueur;
        private double hauteur;
        private float orientation;

        public int IdMur { get => idMur; set => idMur = value; }
        public int IdDirection { get => idDirection; set => idDirection = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public double Longueur { get => longueur; set => longueur = value; }
        public double Hauteur { get => hauteur; set => hauteur = value; }
        public float Orientation { get => orientation; set => orientation = value; }
    }
}
