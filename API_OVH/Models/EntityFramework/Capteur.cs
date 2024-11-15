namespace API_OVH.Models.EntityFramework
{
    public class Capteur
    {
        private int idCapteur;
        private int idSalle;
        private int idTypeMesure;
        private string nomTypeCapteur;
        private string estActif;
        private double xCapteur;
        private double yCapteur;
        private double zCapteur;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdTypeMesure { get => idTypeMesure; set => idTypeMesure = value; }
        public string NomTypeCapteur { get => nomTypeCapteur; set => nomTypeCapteur = value; }
        public string EstActif { get => estActif; set => estActif = value; }
        public double XCapteur { get => xCapteur; set => xCapteur = value; }
        public double YCapteur { get => yCapteur; set => yCapteur = value; }
        public double ZCapteur { get => zCapteur; set => zCapteur = value; }
    }
}
