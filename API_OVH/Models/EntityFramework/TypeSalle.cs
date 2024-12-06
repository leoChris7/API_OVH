using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("typesalle")]
    public partial class TypeSalle
    {
        private ICollection<Salle> salles = new List<Salle>();

        [Key]
        [Column("idtypesalle")]
        public int IdTypeSalle { get; set; }

        [Required]
        [Column("nomtypesalle", TypeName = "varchar(20)")]
        public string NomTypeSalle { get; set; }

        [InverseProperty(nameof(Salle.TypeSalleNavigation))]
        public ICollection<Salle> Salles { get => salles; set => salles = value; }
    }
}
