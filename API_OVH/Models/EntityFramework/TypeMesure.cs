namespace API_OVH.Models.EntityFramework
{
    public class TypeMesure
    {
        private int idTypeMesure;
        private string nomTypeMesure;
        private string sigleTypeMesure;

        public int IdTypeMesure { get => idTypeMesure; set => idTypeMesure = value; }
        public string NomTypeMesure { get => nomTypeMesure; set => nomTypeMesure = value; }
        public string SigleTypeMesure { get => sigleTypeMesure; set => sigleTypeMesure = value; }
    }
}
