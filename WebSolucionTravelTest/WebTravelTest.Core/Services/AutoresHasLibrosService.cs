using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Core.Services
{
    public class AutoresHasLibrosService : IAutoresHasLibrosService
    {
        private readonly IAutoresHasLibrosRepository autoresHasLibrosRepository;
        public AutoresHasLibrosService(IAutoresHasLibrosRepository autoresHasLibrosRepository)
        {
            this.autoresHasLibrosRepository = autoresHasLibrosRepository;
        }
        public async Task<string> Create(AutoresHasLibros autorLibro)
        {
            return await this.autoresHasLibrosRepository.Create(autorLibro);
        }
        public async Task<List<Autores>> GetAutoresByISBNLibro(long isbn)
        {
            return await this.autoresHasLibrosRepository.GetAutoresByISBNLibro(isbn);
        }
        public async Task<bool> Delete(AutoresHasLibros autorLibro)
        {
            return await this.autoresHasLibrosRepository.Delete(autorLibro);
        }
    }
}
