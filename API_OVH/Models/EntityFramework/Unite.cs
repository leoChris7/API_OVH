namespace API_OVH.Models.EntityFramework
{
    public class Unite
    {
        private int idUnite;
        private string nomUnite;
        private string? sigleUnite;

        public int IdUnite { get => idUnite; set => idUnite = value; }
        public string NomUnite { get => nomUnite; set => nomUnite = value; }
        public string? SigleUnite { get => sigleUnite; set => sigleUnite = value; }
    }
}
