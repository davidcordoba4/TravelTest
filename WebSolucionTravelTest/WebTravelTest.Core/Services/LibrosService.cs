using System.Collections.Generic;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories.Interfaces;

namespace WebTravelTest.Core.Services
{
    public class LibrosService : ILibrosService
    {
        private readonly ILibrosRepository librosRepository;
        public LibrosService(ILibrosRepository librosRepository)
        {
            this.librosRepository = librosRepository;
        }
        public async Task<string> Create(Libros libro)
        {
            return await this.librosRepository.Create(libro);
        }
        public async Task<List<Libros>> GetAll()
        {
            return await this.librosRepository.GetAll();
        }
        public async Task<Libros> GetByISBN(long isbn)
        {
            return await this.librosRepository.GetByISBN(isbn);
        }
        public async Task<Editoriales> GetEditorialByISBN(long isbn)
        {
            return await this.librosRepository.GetEditorialByISBN(isbn);
        }
        public async Task<bool> Delete(Libros libro)
        {
            return await this.librosRepository.Delete(libro);
        }
    }
}
