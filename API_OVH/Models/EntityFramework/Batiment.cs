using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_OVH.Models.EntityFramework
{
    public class Batiment
    {
        private int idBatiment;
        private string? nomBatiment;

        public int IdBatiment { get => idBatiment; set => idBatiment = value; }

        public string? NomBatiment { get => nomBatiment; set => nomBatiment = value; }
    }
}
