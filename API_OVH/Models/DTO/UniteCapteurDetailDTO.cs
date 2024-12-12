namespace API_OVH.Models.DTO
{
    public class UniteCapteurDetailDTO
    {
        private UniteDTO unite;
        private CapteurDTO capteur;

        public UniteDTO Unite { get => unite; set => unite = value; }
        public CapteurDTO Capteur { get => capteur; set => capteur = value; }
    }
}
