using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Core.Services
{
    public class EditorialesService : IEditorialesService
    {
        private readonly IEditorialesRepository editorialesRepository;
        public EditorialesService(IEditorialesRepository editorialesRepository)
        {
            this.editorialesRepository = editorialesRepository;
        }
        public async Task<string> Create(Editoriales editorial)
        {
            return await this.editorialesRepository.Create(editorial);
        }
        public async Task<List<Editoriales>> GetAll()
        {
            return await this.editorialesRepository.GetAll();
        }
        public async Task<bool> Delete(Editoriales editorial)
        {
            return await this.editorialesRepository.Delete(editorial);
        }
    }
}
