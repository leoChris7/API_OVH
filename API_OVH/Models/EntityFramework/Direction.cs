using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("DIRECTION")]
    public partial class Direction
    {
        private ICollection<Mur> murs;

        [Key]
        [Column("IDDIRECTION")]
        public int IdDirection { get; set; }

        [Required]
        [Column("LETTRES_DIRECTION", TypeName = "varchar(2)")]
        public string LettresDirection { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Mur.DirectionNavigation))]
        public ICollection<Mur> Murs { get => murs; set => murs = value; }
    }
}
