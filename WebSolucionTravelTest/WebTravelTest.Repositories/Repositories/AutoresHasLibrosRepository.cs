using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Entities.DbContexts;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Repositories.Repositories
{
    public class AutoresHasLibrosRepository : IAutoresHasLibrosRepository
    {
        public const string registroExiste = "Ya existe un registro con la misma información";
        private readonly IApplicationDbContext _dbcontext;
        public AutoresHasLibrosRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<string> Create(AutoresHasLibros autorLibro)
        {
            if (_dbcontext.AutoresHasLibros.FirstOrDefault(autLib => autLib.LibrosIsbn == autorLibro.LibrosIsbn && autLib.AutoresId == autorLibro.AutoresId) != null)
            {
                return registroExiste;
            }
            _dbcontext.AutoresHasLibros.Add(autorLibro);
            await _dbcontext.SaveChanges();
            return string.Empty;
        }
        public async Task<List<Autores>> GetAutoresByISBNLibro(long isbn)
        {
            var autoresLibros = await _dbcontext.AutoresHasLibros.Where(autLib => autLib.LibrosIsbn == isbn).Include(al => al.Autores).Include(al => al.LibrosIsbnNavigation).ToListAsync();
            var autores = autoresLibros.Select(x => x.Autores).ToList();
            return autores;
        }
        public async Task<bool> Delete(AutoresHasLibros autorLibro)
        {
            _dbcontext.AutoresHasLibros.Remove(autorLibro);
            await _dbcontext.SaveChanges();
            return true;
        }
    }
}
