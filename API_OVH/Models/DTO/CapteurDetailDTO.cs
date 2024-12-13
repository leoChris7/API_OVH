﻿using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int IdCapteur { get => idCapteur; set => idCapteur = value; }

        [Required]
        [MaxLength(25, ErrorMessage = "Le nom ne doit pas dépasser 25 caractères.")]
        public string NomCapteur { get => nomCapteur; set => nomCapteur = value; }

        [Length(3, 3, ErrorMessage = "L'état doit être de 3 caractères")]
        [EtatValidationAttribute(ErrorMessage = "Etat invalide")]
        public string EstActif { get => estActif; set => estActif = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal XCapteur { get => xCapteur; set => xCapteur = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal YCapteur { get => yCapteur; set => yCapteur = value; }

        [Range(0.0, 999999999.9, ErrorMessage = "Les coordonnées doivent être comprises entre 0 et 999,999,999.9cm.")]
        public decimal ZCapteur { get => zCapteur; set => zCapteur = value; }

        public List<UniteDTO> Unites { get => unites; set => unites = value; }
        public MurSansNavigationDTO Mur { get => mur; set => mur = value; }
        public SalleSansNavigationDTO Salle { get => salle; set => salle = value; }
    }
}
