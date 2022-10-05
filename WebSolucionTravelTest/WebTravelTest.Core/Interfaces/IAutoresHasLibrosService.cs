using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Core.Interfaces
{
    public interface IAutoresHasLibrosService
    {
        Task<string> Create(AutoresHasLibros autorLibro);
        Task<List<Autores>> GetAutoresByISBNLibro(long isbn);
        Task<bool> Delete(AutoresHasLibros autorLibro);
    }
}
