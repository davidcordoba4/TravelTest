using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Repositories.Interfaces
{
    public interface IAutoresHasLibrosRepository
    {
        Task<string> Create(AutoresHasLibros autorLibro);
        Task<List<Autores>> GetAutoresByISBNLibro(long isbn);
        Task<bool> Delete(AutoresHasLibros autorLibro);
    }
}
