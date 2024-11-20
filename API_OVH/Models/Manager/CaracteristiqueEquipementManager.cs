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
    /// Manager pour gérer les opérations liées aux CaracteristiqueEquipements
    /// </summary>
    public class CaracteristiqueEquipementManager : IDataRepository<CaracteristiqueEquipement>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public CaracteristiqueEquipementManager() { }
        public CaracteristiqueEquipementManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de toutes les caracteristiques equipement de façon asynchrone
        /// </summary>
        /// <returns>La liste des caracteristiques equipement</returns>
        public async Task<ActionResult<IEnumerable<CaracteristiqueEquipement>>> GetAllAsync()
        {
            return await dbContext.CaracteristiquesEquipement.ToListAsync();
        }

        /// <summary>
        /// Retourne une caracteristique equipement selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la caracteristique equipement</param>
        /// <returns>La caracteristique equipement correspondant à l'ID</returns>
        public async Task<ActionResult<CaracteristiqueEquipement>> GetByIdAsync(int id)
        {
            return await dbContext.CaracteristiquesEquipement.FirstOrDefaultAsync(t => t.IdCaracteristique == id);
        }

        /// <summary>
        /// Retourne une caracteristique equipement selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom de la caracteristique equipement </param>
        /// <returns>La caracteristique equipement correspondante au nom spécifié</returns>
        public async Task<ActionResult<CaracteristiqueEquipement>> GetByStringAsync(string nom)
        {
            return await dbContext.CaracteristiquesEquipement.FirstOrDefaultAsync(t => t.NomCaracteristique.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute une caracteristique equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">caracteristique equipement à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(CaracteristiqueEquipement entity)
        {
            await dbContext.CaracteristiquesEquipement.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Met à jour une caracteristique equipement de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">caracteristique equipement à mettre à jour</param>
        /// <param name="entity">caracteristique equipement avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(CaracteristiqueEquipement CaracteristiqueEquipement, CaracteristiqueEquipement entity)
        {
            dbContext.Entry(CaracteristiqueEquipement).State = EntityState.Modified;
            CaracteristiqueEquipement.IdCaracteristique = entity.IdCaracteristique;
            CaracteristiqueEquipement.NomCaracteristique = entity.NomCaracteristique;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un caracteristique equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">caracteristique equipement à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(CaracteristiqueEquipement CaracteristiqueEquipement)
        {
            dbContext.CaracteristiquesEquipement.Remove(CaracteristiqueEquipement);
            await dbContext.SaveChangesAsync();
        }
    }
}
