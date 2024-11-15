namespace API_OVH.Models.EntityFramework
{
    public class CaracteristiqueEquipement
    {
        private int idcaracteristique;
        private string nomcaracteristique;

        public int Idcaracteristique { get => idcaracteristique; set => idcaracteristique = value; }
        public string Nomcaracteristique { get => nomcaracteristique; set => nomcaracteristique = value; }
    }
}
