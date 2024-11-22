﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Salle
    {
        private int idSalle;
        private int idBatiment;
        private int idtypeSalle;
        private string? nomSalle;
        private double superficieSalle;
        private Batiment? batimentNavigation;
        private TypeSalle? typeSalleNavigation;
        private ICollection<Capteur> capteurs = new List<Capteur>();
        private ICollection<Equipement> equipements = new List<Equipement>();
        private ICollection<Mur> murs = new List<Mur>();

        public int IdSalle { get => idSalle; set => idSalle = value; }
        public int IdBatiment { get => idBatiment; set => idBatiment = value; }
        public int IdTypeSalle { get => idtypeSalle; set => idtypeSalle = value; }
        public string? NomSalle { get => nomSalle; set => nomSalle = value; }
        public double SuperficieSalle { get => superficieSalle; set => superficieSalle = value; }

        [JsonIgnore]
        [ForeignKey(nameof(idBatiment))]
        [InverseProperty(nameof(Batiment.Salles))]
        public Batiment? BatimentNavigation { get => batimentNavigation; set => batimentNavigation = value; }

        [JsonIgnore]
        [ForeignKey(nameof(IdTypeSalle))]
        [InverseProperty(nameof(TypeSalle.Salles))]
        public TypeSalle? TypeSalleNavigation { get => typeSalleNavigation; set => typeSalleNavigation = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Capteur.SalleNavigation))]
        public virtual ICollection<Capteur> Capteurs { get => capteurs; set => capteurs = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Equipement.SalleNavigation))]
        public virtual ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Mur.SalleNavigation))]
        public virtual ICollection<Mur> Murs { get => murs; set => murs = value; }
    }
}
