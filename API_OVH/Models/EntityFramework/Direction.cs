using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("direction")]
    public partial class Direction
    {
        private ICollection<Mur> murs;

        [Key]
        [Column("iddirection")]
        public short IdDirection { get; set; }

        [Required]
        [Length(1, 2, ErrorMessage = "La cardinalité doit être de 1 (N) ou 2 caractère (NO)")]
        [Column("lettres_direction", TypeName = "varchar(2)")]
        public string LettresDirection { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Mur.DirectionNavigation))]
        public ICollection<Mur> Murs { get => murs; set => murs = value; }
    }
}
