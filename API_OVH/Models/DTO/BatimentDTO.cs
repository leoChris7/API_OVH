namespace API_OVH.Models.DTO
{
    public class BatimentDTO
    {
        private string nombatiment;
        private int nbsalle;

        public string Nombatiment { get => nombatiment; set => nombatiment = value; }
        public int Nbsalle { get => nbsalle; set => nbsalle = value; }
    }
}
