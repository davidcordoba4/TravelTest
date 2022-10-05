using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Core.Services
{
    public class AutoresService : IAutoresService
    {
        private readonly IAutoresRepository autoresRepository;
        public AutoresService(IAutoresRepository autoresRepository)
        {
            this.autoresRepository = autoresRepository;
        }
        public async Task<string> Create(Autores autor)
        {
            return await this.autoresRepository.Create(autor);
        }
        public async Task<List<Autores>> GetAll()
        {
            return await this.autoresRepository.GetAll();
        }
        public async Task<Autores> GetById(int idAutor)
        {
            return await this.autoresRepository.GetById(idAutor);
        }
        public async Task<bool> Delete(Autores autor)
        {
            return await this.autoresRepository.Delete(autor);
        }
    }
}
