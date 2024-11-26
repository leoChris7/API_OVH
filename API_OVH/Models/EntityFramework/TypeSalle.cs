using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("TYPESALLE")]
    public partial class TypeSalle
    {
        private ICollection<Salle> salles = new List<Salle>();

        [Key]
        [Column("IDTYPESALLE")]
        public int IdTypeSalle { get; set; }

        [Required]
        [Column("NOMTYPESALLE", TypeName = "varchar(20)")]
        public string NomTypeSalle { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Salle.TypeSalleNavigation))]
        public ICollection<Salle> Salles { get => salles; set => salles = value; }
    }
}
