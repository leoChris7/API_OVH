﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class TypeEquipement
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;
        private ICollection<Equipement> equipements;

        public int IdTypeEquipement { get => idTypeEquipement; set => idTypeEquipement = value; }
        public string NomTypeEquipement { get => nomTypeEquipement; set => nomTypeEquipement = value; }

        [JsonIgnore]
        [InverseProperty(nameof(Equipement.TypeEquipementNavigation))]
        public ICollection<Equipement> Equipements { get => equipements; set => equipements = value; }
    }
}
