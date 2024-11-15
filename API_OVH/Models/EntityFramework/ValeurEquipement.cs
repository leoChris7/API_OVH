namespace API_OVH.Models.EntityFramework
{
    public class ValeurEquipement
    {
        private int idcaracteristique;
        private int idequipement;
        private int idunite;
        private double valeur;

        public int Idcaracteristique { get => idcaracteristique; set => idcaracteristique = value; }
        public int Idequipement { get => idequipement; set => idequipement = value; }
        public int Idunite { get => idunite; set => idunite = value; }
        public double Valeur { get => valeur; set => valeur = value; }
    }
}
