namespace API_OVH.Models.EntityFramework
{
    public class Batiment
    {
        private int idbatiment;
        private string? nombatiment;

        public int Idbatiment { get => idbatiment; set => idbatiment = value; }
        public string? Nombatiment { get => nombatiment; set => nombatiment = value; }
    }
}
