using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Repositories.Interfaces
{
    public interface ILibrosRepository
    {
        Task<string> Create(Libros libro);
        Task<List<Libros>> GetAll();
        Task<Libros> GetByISBN(long isbn);
        Task<Editoriales> GetEditorialByISBN(long isbn);
        Task<bool> Delete(Libros libro);
    }
}
