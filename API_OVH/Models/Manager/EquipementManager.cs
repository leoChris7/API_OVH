using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;

namespace API_OVH.Models.DataManager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux Equipements
    /// </summary>
    public class EquipementManager : IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO, EquipementSansNavigationDTO>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public EquipementManager() { }
        public EquipementManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les Equipements sous forme de DTO simplifié de façon asynchrone
        /// </summary>
        /// <returns>La liste des Equipements</returns>
        public async Task<ActionResult<IEnumerable<EquipementDTO>>> GetAllAsync()
        {
            return await dbContext.Equipements
                .ProjectTo<EquipementDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Retourne un DTO détaillé de l'Equipement selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de l'Equipement</param>
        /// <returns>L'Equipement correspondant à l'ID</returns>
        public async Task<ActionResult<EquipementDetailDTO>> GetByIdAsync(int id)
        {
            return await dbContext.Equipements
                .Where(t => t.IdEquipement == id)
                .ProjectTo<EquipementDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retourne un Equipement selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de l'Equipement</param>
        /// <returns>L'Equipement correspondant à l'ID</returns>
        public async Task<ActionResult<Equipement>> GetByIdWithoutDTOAsync(int id)
        {
            return await dbContext.Equipements.FirstOrDefaultAsync(t => t.IdEquipement == id);
        }

        /// <summary>
        /// Retourne un Equipement selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom de l'equipement </param>
        /// <returns>L'equipement correspondant au nom spécifié</returns>
        public async Task<ActionResult<Equipement>> GetByStringAsync(string nom)
        {
            return await dbContext.Equipements.FirstOrDefaultAsync(t => t.NomEquipement.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute un Equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">Equipement à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(EquipementSansNavigationDTO entity)
        {
            var equipement = mapper.Map<Mur>(entity);

            await dbContext.Equipements.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Met à jour un Equipement de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Equipement à mettre à jour</param>
        /// <param name="entity">Equipement avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Equipement Equipement, Equipement entity)
        {
            dbContext.Entry(Equipement).State = EntityState.Modified;
            Equipement.IdEquipement = entity.IdEquipement;
            Equipement.IdMur = entity.IdMur;
            Equipement.IdTypeEquipement = entity.IdTypeEquipement;
            Equipement.NomEquipement = entity.NomEquipement;
            Equipement.Longueur = entity.Longueur;
            Equipement.Largeur = entity.Largeur;
            Equipement.Hauteur = entity.Hauteur;
            Equipement.EstActif = entity.EstActif;
            Equipement.XEquipement = entity.XEquipement;
            Equipement.YEquipement = entity.YEquipement;
            Equipement.ZEquipement = entity.ZEquipement;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un Equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">Equipement à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Equipement Equipement)
        {
            dbContext.Equipements.Remove(Equipement);
            await dbContext.SaveChangesAsync();
        }
    }
}
