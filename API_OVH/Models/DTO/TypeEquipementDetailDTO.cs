using API_OVH.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_OVH.Models.DTO
{
    public class TypeEquipementDetailDTO
    {
        private int idTypeEquipement;
        private string nomTypeEquipement;
        private ICollection<EquipementSansNavigationDTO> equipements;

        [Required]
        public int IdTypeEquipement
        {
            get
            {
                return this.idTypeEquipement;
            }

            set
            {
                this.idTypeEquipement = value;
            }
        }

        [Required]
        [MaxLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères.")]
        public string NomTypeEquipement
        {
            get
            {
                return this.nomTypeEquipement;
            }

            set
            {
                this.nomTypeEquipement = value;
            }
        }

        public ICollection<EquipementSansNavigationDTO> Equipements
        {
            get
            {
                return this.equipements;
            }

            set
            {
                this.equipements = value;
            }
        }
    }
}
