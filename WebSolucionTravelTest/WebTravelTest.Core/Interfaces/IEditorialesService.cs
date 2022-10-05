using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Core.Interfaces
{
    public interface IEditorialesService
    {
        Task<string> Create(Editoriales editorial);
        Task<List<Editoriales>> GetAll();
        Task<bool> Delete(Editoriales autor);
    }
}
