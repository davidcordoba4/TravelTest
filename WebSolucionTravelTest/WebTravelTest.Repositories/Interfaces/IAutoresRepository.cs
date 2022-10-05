using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Repositories.Interfaces
{
    public interface IAutoresRepository
    {
        Task<string> Create(Autores autor);
        Task<List<Autores>> GetAll();
        Task<Autores> GetById(int id);
        Task<bool> Delete(Autores autor);
    }
}
