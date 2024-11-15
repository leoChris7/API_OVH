using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.EntityFramework
{
    [Table("batiment")]
    public class Batiment
    {
        private int idbatiment;
        private string? nombatiment;

        [Column("idbatiment")]
        public int Idbatiment { get => idbatiment; set => idbatiment = value; }

        [Column("nombatiment")]
        public string? Nombatiment { get => nombatiment; set => nombatiment = value; }
    }
}
