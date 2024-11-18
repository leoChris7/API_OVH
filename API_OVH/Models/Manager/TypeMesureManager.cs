using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;

namespace TDNote.Models.DataManager
{
    public class TypesMesureManager : IDataRepository<TypeMesure>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public TypesMesureManager() { }
        public TypesMesureManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        //Récupère tous les types de salle
        public async Task<ActionResult<IEnumerable<TypeMesure>>> GetAllAsync()
        {
            return await dbContext.TypesMesure.ToListAsync();
        }

        //Récupère un type de salle à l'aide de son ID
        public async Task<ActionResult<TypeMesure>> GetByIdAsync(int id)
        {
            return await dbContext.TypesMesure.FirstOrDefaultAsync(t => t.IdTypeMesure == id);
        }

        //Récupère un type de salle à l'aide du nom du type
        public async Task<ActionResult<TypeMesure>> GetByStringAsync(string nom)
        {
            return await dbContext.TypesMesure.FirstOrDefaultAsync(t => t.NomTypeMesure.ToUpper() == nom.ToUpper());
        }

        //Ajoute un nouveau type de salle
        public async Task AddAsync(TypeMesure entity)
        {
            await dbContext.TypesMesure.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //Met à jour les données d'un type de salle pour qu'elles correspondent aux données du type de salle passé en paramètre
        public async Task UpdateAsync(TypeMesure TypeMesure, TypeMesure entity)
        {
            dbContext.Entry(TypeMesure).State = EntityState.Modified;
            TypeMesure.IdTypeMesure = entity.IdTypeMesure;
            TypeMesure.NomTypeMesure = entity.NomTypeMesure;
            dbContext.SaveChangesAsync();
        }

        //Supprime le type de salle passé en paramètre
        public async Task DeleteAsync(TypeMesure TypeMesure)
        {
            dbContext.TypesMesure.Remove(TypeMesure);
            await dbContext.SaveChangesAsync();
        }
    }
}
