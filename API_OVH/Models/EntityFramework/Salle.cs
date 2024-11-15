namespace API_OVH.Models.EntityFramework
{
    public class Salle
    {
        private int idsalle;
        private int idbatiment;
        private int idtypesalle;
        private string? nomsalle;
        private double superficieSalle;

        public int Idsalle { get => idsalle; set => idsalle = value; }
        public int Idbatiment { get => idbatiment; set => idbatiment = value; }
        public int Idtypesalle { get => idtypesalle; set => idtypesalle = value; }
        public string? Nomsalle { get => nomsalle; set => nomsalle = value; }
        public double SuperficieSalle { get => superficieSalle; set => superficieSalle = value; }
    }
}
