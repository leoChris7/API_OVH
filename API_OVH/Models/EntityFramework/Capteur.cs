using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Capteur
    {
        private int idCapteur;
        private int idSalle;
        private int idTypeMesure;
        private string nomCapteur;
        private string estActif;
        private double xCapteur;
        private double yCapteur;
        private double zCapteur;
        private Salle? salleNavigation;
        private TypeMesure? typeMesureNavigation;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdTypeMesure { get => idTypeMesure; set => idTypeMesure = value; }
        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }
        public string EstActif { get => estActif; set => estActif = value; }
        public double XCapteur { get => xCapteur; set => xCapteur = value; }
        public double YCapteur { get => yCapteur; set => yCapteur = value; }
        public double ZCapteur { get => zCapteur; set => zCapteur = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdSalle))]
        [InverseProperty(nameof(Salle.Capteurs))]
        public Salle? SalleNavigation { get => salleNavigation; set => salleNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeMesure))]
        [InverseProperty(nameof(TypeMesure.Capteurs))]
        public TypeMesure? TypeMesureNavigation { get => typeMesureNavigation; set => typeMesureNavigation = value; }
    }
}
