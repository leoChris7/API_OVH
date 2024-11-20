namespace API_OVH.Models.EntityFramework
{
    public class CaracteristiqueEquipement
    {
        private int idCaracteristique;
        private string nomCaracteristique;

        public int IdCaracteristique { get => idCaracteristique; set => idCaracteristique = value; }
        public string NomCaracteristique { get => nomCaracteristique; set => nomCaracteristique = value; }
    }
}
