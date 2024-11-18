namespace API_OVH.Models.DTO
{
    public class EquipementDTO
    {
        private string nomequipement;
        private string mesureEquipementFormatte; // Caracteristique: ValeurEquipement + Sigle Unite
        private string nomSalleEquipement;
        private string nomTypeEquipement;

        public string Nomequipement { get => nomequipement; set => nomequipement = value; }
        public string MesureEquipementFormatte { get => mesureEquipementFormatte; set => mesureEquipementFormatte = value; }
        public string NomSalleEquipement { get => nomSalleEquipement; set => nomSalleEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }
    }
}
