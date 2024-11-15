namespace API_OVH.Models.EntityFramework
{
    public class Equipement
    {
        private int idequipement;
        private int idsalle;
        private int idtypeequipement;
        private string? nomEquipement;
        private double xEquipement;
        private double yEquipement;
        private double zEquipement;

        public int Idequipement { get => idequipement; set => idequipement = value; }
        public int Idsalle { get => idsalle; set => idsalle = value; }
        public int Idtypeequipement { get => idtypeequipement; set => idtypeequipement = value; }
        public string? NomEquipement { get => nomEquipement; set => nomEquipement = value; }
        public double XEquipement { get => xEquipement; set => xEquipement = value; }
        public double YEquipement { get => yEquipement; set => yEquipement = value; }
        public double ZEquipement { get => zEquipement; set => zEquipement = value; }
    }
}
