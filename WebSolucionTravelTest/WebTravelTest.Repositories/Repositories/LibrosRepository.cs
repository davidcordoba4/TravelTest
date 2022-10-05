using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Entities.DbContexts;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Repositories.Repositories
{
    public class LibrosRepository : ILibrosRepository
    {
        private readonly IApplicationDbContext _dbcontext;
        public const string registroExiste = "Ya existe un registro para el mismo ISBN";
        public LibrosRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<string> Create(Libros libro)
        {
            if (_dbcontext.Libros.FirstOrDefault(lib => lib.Isbn == libro.Isbn) != null)
            {
                return registroExiste;
            }
            _dbcontext.Libros.Add(libro);
            await _dbcontext.SaveChanges();
            return string.Empty;
        }
        public async Task<List<Libros>> GetAll()
        {
            var libros = await _dbcontext.Libros.Include(l => l.Editoriales).Include(l => l.AutoresHasLibros).ToListAsync<Libros>();
            return libros;
        }
        public async Task<Libros> GetByISBN(long isbn)
        {
            var libro = await _dbcontext.Libros.Where(lib => lib.Isbn == isbn).Include(l => l.Editoriales).Include(l => l.AutoresHasLibros).Include("AutoresHasLibros.Autores").FirstOrDefaultAsync();
            return libro;
        }
        public async Task<Editoriales> GetEditorialByISBN(long isbn)
        {
            var libro = await _dbcontext.Libros.Include(l => l.Editoriales).Include(l => l.AutoresHasLibros).FirstOrDefaultAsync(lib => lib.Isbn == isbn);
            return libro.Editoriales;
        }
        public async Task<bool> Delete(Libros libro)
        {
            _dbcontext.Libros.Remove(libro);
            await _dbcontext.SaveChanges();
            return true;
        }
    }
}
