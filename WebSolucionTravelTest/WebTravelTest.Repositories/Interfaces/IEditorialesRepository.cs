using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Repositories.Interfaces
{
    public interface IEditorialesRepository
    {
        Task<string> Create(Editoriales editorial);
        Task<List<Editoriales>> GetAll();
        Task<bool> Delete(Editoriales autor);
    }
}
