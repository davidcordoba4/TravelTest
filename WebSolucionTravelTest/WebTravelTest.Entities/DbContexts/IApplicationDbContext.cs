using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Entities.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Autores> Autores { get; set; }
        DbSet<AutoresHasLibros> AutoresHasLibros { get; set; }
        DbSet<Editoriales> Editoriales { get; set; }
        DbSet<Libros> Libros { get; set; }
        Task<int> SaveChanges();
    }
}
