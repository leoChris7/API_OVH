using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("batiment")]
    public partial class Batiment
    {
        private ICollection<Salle> salles = new List<Salle>();

        [Key]
        [Column("idbatiment")]
        public int IdBatiment { get; set; }

        [Required]
        [Column("nombatiment", TypeName = "varchar(20)")]
        public string NomBatiment { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Salle.BatimentNavigation))]
        public ICollection<Salle> Salles { get => salles; set => salles = value; }
    }
}
