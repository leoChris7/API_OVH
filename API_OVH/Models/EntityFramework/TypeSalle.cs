namespace API_OVH.Models.EntityFramework
{
    public class TypeSalle
    {
        private int idTypeSalle;
        private string nomTypeSalle;

        public int IdTypeSalle { get => idTypeSalle; set => idTypeSalle = value; }
        public string NomTypeSalle { get => nomTypeSalle; set => nomTypeSalle = value; }
    }
}
