using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Core.Interfaces
{
    public interface IAutoresService
    {
        Task<string> Create(Autores autor);
        Task<List<Autores>> GetAll();
        Task<Autores> GetById(int id);
        Task<bool> Delete(Autores autor);
    }
}
