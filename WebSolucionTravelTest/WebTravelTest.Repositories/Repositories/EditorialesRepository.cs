using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Entities.DbContexts;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Repositories.Repositories
{
    public class EditorialesRepository : IEditorialesRepository
    {
        private readonly IApplicationDbContext _dbcontext;
        public const string registroExiste = "Ya existe un registro con los mismos datos";
        public EditorialesRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<string> Create(Editoriales editorial)
        {
            if (_dbcontext.Editoriales.FirstOrDefault(edit => edit.Nombre == editorial.Nombre && edit.Sede == editorial.Sede) != null)
            {
                return registroExiste;
            }
            _dbcontext.Editoriales.Add(editorial);
            await _dbcontext.SaveChanges();
            return string.Empty;
        }
        public async Task<List<Editoriales>> GetAll()
        {
            var editoriales = await _dbcontext.Editoriales.ToListAsync<Editoriales>();
            return editoriales;
        }
        public async Task<bool> Delete(Editoriales editorial)
        {
            _dbcontext.Editoriales.Remove(editorial);
            await _dbcontext.SaveChanges();
            return true;
        }
    }
}
