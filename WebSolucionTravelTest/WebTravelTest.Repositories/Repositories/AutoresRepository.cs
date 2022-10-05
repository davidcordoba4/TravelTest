using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Entities.DbContexts;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Repositories.Repositories
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly IApplicationDbContext _dbcontext;
        public const string registroExiste = "Ya existe un registro con los mismos datos";
        public AutoresRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<string> Create(Autores autor)
        {
            if (_dbcontext.Autores.FirstOrDefault(aut => aut.Nombre == autor.Nombre && aut.Apellidos == autor.Apellidos) != null)
            {
                return registroExiste;
            }
            _dbcontext.Autores.Add(autor);
            await _dbcontext.SaveChanges();
            return string.Empty;
        }
        public async Task<List<Autores>> GetAll()
        {
            var autores = await _dbcontext.Autores.ToListAsync<Autores>();
            return autores;
        }
        public async Task<Autores> GetById(int idAutor)
        {
            var autor = await _dbcontext.Autores.Where(aut => aut.Id == idAutor).FirstOrDefaultAsync();
            return autor;
        }
        public async Task<bool> Delete(Autores autor)
        {
            _dbcontext.Autores.Remove(autor);
            await _dbcontext.SaveChanges();
            return true;
        }
    }
}
