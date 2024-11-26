using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_OVH.Models.EntityFramework
{
    [Table("UNITE")]
    public partial class Unite
    {
        private ICollection<UniteCapteur> unitesCapteur = new List<UniteCapteur>();

        [Key]
        [Column("IDUNITE")]
        public int IdUnite { get; set; }

        [Required]
        [Column("NOMUNITE", TypeName = "varchar(50)")]
        public string NomUnite { get; set; }

        [Column("SIGLEUNITE", TypeName = "varchar(10)")]
        public string SigleUnite { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(UniteCapteur.UniteNavigation))]
        public virtual ICollection<UniteCapteur> UnitesCapteur { get => unitesCapteur; set => unitesCapteur = value; }
    }
}
