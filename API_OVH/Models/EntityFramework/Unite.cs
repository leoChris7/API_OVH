﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("unite")]
    public partial class Unite
    {
        private ICollection<UniteCapteur> unitesCapteur = new List<UniteCapteur>();

        [Key]
        [Column("idunite")]
        public int IdUnite { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom de l'unité ne doit pas dépasser 50 caractères.")]
        [Required]
        [Column("nomunite", TypeName = "varchar(50)")]
        public string NomUnite { get; set; }

        [MaxLength(10, ErrorMessage = "Le sigle ne doit pas dépasser 10 caractères.")]
        [Column("sigleunite", TypeName = "varchar(10)")]
        public string? SigleUnite { get; set; }

        [InverseProperty(nameof(UniteCapteur.UniteNavigation))]
        public virtual ICollection<UniteCapteur> UnitesCapteur { get => unitesCapteur; set => unitesCapteur = value; }
    }
}
