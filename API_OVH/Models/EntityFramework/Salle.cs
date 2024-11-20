namespace API_OVH.Models.EntityFramework
{
    public class Salle
    {
        private int idSalle;
        private int idBatiment;
        private int idtypeSalle;
        private string? nomSalle;
        private double superficieSalle;

        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }
        public int IdTypeSalle { get => idtypeSalle; set => idtypeSalle = value; }
        public string? NomSalle { get => nomSalle; set => nomSalle = value; }
        public double SuperficieSalle { get => superficieSalle; set => superficieSalle = value; }
    }
}
