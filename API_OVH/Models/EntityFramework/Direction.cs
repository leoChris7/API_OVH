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
        private ICollection<Salle> salles;

        public int IdDirection { get => idDirection; set => idDirection = value; }
        public string LettresDirection { get => lettresDirection; set => lettresDirection = value; }
        [JsonIgnore]
        [InverseProperty(nameof(Mur.DirectionNavigation))]
        public ICollection<Salle> Murs { get => salles; set => salles = value; }
    }
}
