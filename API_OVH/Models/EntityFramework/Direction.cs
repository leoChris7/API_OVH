using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    public partial class Direction
    {
        private int idDirection;
        private string lettresDirection;
        private ICollection<Mur> murs;

        [Key] // Indique que cette propriété est la clé primaire
        public int IdDirection { get; set; }

        [Column(TypeName = "varchar(2)")]
        [Required]
        public string LettresDirection { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Mur.DirectionNavigation))]
        public ICollection<Mur> Murs { get => murs; set => murs = value; }
    }
}
