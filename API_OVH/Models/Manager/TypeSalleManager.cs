using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;

namespace TDNote.Models.DataManager
{
    public class TypesSalleManager : IDataRepository<TypeSalle>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public TypesSalleManager() { }
        public TypesSalleManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        //Récupère tous les types de salle
        public async Task<ActionResult<IEnumerable<TypeSalle>>> GetAllAsync()
        {
            return await dbContext.TypesSalle.ToListAsync();
        }

        //Récupère un type de salle à l'aide de son ID
        public async Task<ActionResult<TypeSalle>> GetByIdAsync(int id)
        {
            return await dbContext.TypesSalle.FirstOrDefaultAsync(t => t.IdTypeSalle == id);
        }

        //Récupère un type de salle à l'aide du nom du type
        public async Task<ActionResult<TypeSalle>> GetByStringAsync(string nom)
        {
            return await dbContext.TypesSalle.FirstOrDefaultAsync(t => t.NomTypeSalle.ToUpper() == nom.ToUpper());
        }

        //Ajoute un nouveau type de salle
        public async Task AddAsync(TypeSalle entity)
        {
            await dbContext.TypesSalle.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //Met à jour les données d'un type de salle pour qu'elles correspondent aux données du type de salle passé en paramètre
        public async Task UpdateAsync(TypeSalle TypeSalle, TypeSalle entity)
        {
            dbContext.Entry(TypeSalle).State = EntityState.Modified;
            TypeSalle.IdTypeSalle = entity.IdTypeSalle;
            TypeSalle.NomTypeSalle = entity.NomTypeSalle;
            dbContext.SaveChangesAsync();
        }

        //Supprime le type de salle passé en paramètre
        public async Task DeleteAsync(TypeSalle TypeSalle)
        {
            dbContext.TypesSalle.Remove(TypeSalle);
            await dbContext.SaveChangesAsync();
        }
    }
}
