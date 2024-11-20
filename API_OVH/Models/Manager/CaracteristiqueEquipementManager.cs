using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionProduit_API.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux caracteristiqueequipements.
    /// </summary>
    public class CaracteristiqueEquipementManager : IDataRepository<CaracteristiqueEquipement>
    {
        private readonly SAE5_BD_OVH_DbContext _context;
        private readonly IMapper _mapper;

        [ActivatorUtilitiesConstructor]
        public CaracteristiqueEquipementManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CaracteristiqueEquipementManager()
        {
        }

        /// <summary>
        /// Retourne la liste des caracteristiqueequipements de façon asynchrone
        /// </summary>
        /// <returns>La liste des caracteristiqueequipements</returns>
        public async Task<ActionResult<IEnumerable<CaracteristiqueEquipement>>> GetAllAsync()
        {
            var caracteristiqueEquipements = await _context.CaracteristiqueEquipements.ToListAsync();

            return caracteristiqueEquipements;
        }

        /// <summary>
        /// Retourne une caracteristique d'équipement selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la caracteristique d'équipement</param>
        /// <returns>La caracteristique d'équipement correspondant à l'ID</returns>
        public async Task<ActionResult<CaracteristiqueEquipement>> GetByIdAsync(int id)
        {
            var laCaracteristiqueEquipement = await _context.CaracteristiqueEquipements.FirstOrDefaultAsync(x => x.Idcaracteristique == id);

            // S'il n'est pas trouvé
            if (laCaracteristiqueEquipement == null)
            {
                return new NotFoundResult();
            }

            return laCaracteristiqueEquipement;
        }

        /// <summary>
        /// Retourne une caracteristique d'équipement selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du type de caracteristique d'équipement</param>
        /// <returns>Le caracteristique d'équipement correspondant au nom spécifié</returns>
        public async Task<ActionResult<CaracteristiqueEquipement>> GetByStringAsync(string str)
        {
            var laCaracteristiqueEquipement = await _context.CaracteristiqueEquipements.FirstOrDefaultAsync(x => x.Nomcaracteristique == str);

            // Si non trouvé
            if (laCaracteristiqueEquipement == null)
            {
                return new NotFoundResult();
            }

            return laCaracteristiqueEquipement;
        }

        /// <summary>
        /// Ajoute une caracteristique d'équipement de façon asynchrone
        /// </summary>
        /// <param name="entity">CaracteristiqueEquipement à rajouter</param>
        /// <returns>Le caracteristique d'équipement créée</returns>
        public async Task AddAsync(CaracteristiqueEquipement entity)
        {
            await _context.CaracteristiqueEquipements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un caracteristique d'équipement de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">CaracteristiqueEquipement à mettre à jour</param>
        /// <param name="entity">CaracteristiqueEquipement avec les valeurs mis à jour</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(CaracteristiqueEquipement entityToUpdate, CaracteristiqueEquipement entity)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.Idcaracteristique = entity.Idcaracteristique;
            entityToUpdate.Nomcaracteristique = entity.Nomcaracteristique;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer une caractéristique de façon asynchrone
        /// </summary>
        /// <param name="entity">La caractéristique à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(CaracteristiqueEquipement entity)
        {
            _context.CaracteristiqueEquipements.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}