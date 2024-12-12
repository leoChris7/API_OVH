﻿using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.DTO
{
    public class CapteurDetailDTO
    {
        private int idCapteur;
        private string nomCapteur;
        private string estActif;
        private decimal xCapteur;
        private decimal yCapteur;
        private decimal zCapteur;
        private List<UniteDTO> unites;
        private MurSansNavigationDTO mur;
        private SalleSansNavigationDTO salle;

        public int IdCapteur { get => idCapteur; set => idCapteur = value; }
        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }
        public string EstActif { get => estActif; set => estActif = value; }
        public decimal XCapteur { get => xCapteur; set => xCapteur = value; }
        public decimal YCapteur { get => yCapteur; set => yCapteur = value; }
        public decimal ZCapteur { get => zCapteur; set => zCapteur = value; }
        public List<UniteDTO> Unites { get => unites; set => unites = value; }
        public MurSansNavigationDTO Mur { get => mur; set => mur = value; }
        public SalleSansNavigationDTO Salle { get => salle; set => salle = value; }
    }
}
