using NUnit.Framework;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories;
using WebTravelTest.Repositories.Repositories;
using WebTravelTest.Core.Services;
using WebTravelTest.Repositories.Interfaces;
using WebTravelTest.Entities.DbContexts;

namespace WebTravelTest.UnitTest
{
    public class UnitTestAutoresLibros
    {
        // Clase Base de Datos Fake
        private SqLiteDbFake sqLiteDbFake;
        private int editorialIdCreado;
        private long? libroIsbnCreado;
        private int autorIdCreado;
        [SetUp]
        public void Setup()
        {
            if(sqLiteDbFake == null)
            {
                sqLiteDbFake = new SqLiteDbFake();
            }
        }

        [Test]
        public void Test1AsignarAutorLibro()
        {
            var autor = new Autores() { Nombre = "Carlos", Apellidos = "Cardona" };
            var editorial = new Editoriales() { Nombre = "Az Editora", Sede = "Buenos Aires" };
            var libro = new Libros() { Isbn = 5465465465465, Titulo = "Un viaje por el conocimiento humano", Sinopsis = "Sinopsis un viaje por el conocimiento humano", NPaginas = "2145567" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var librosService = this.LibrosService(context);
                var editorialesService = this.EditorialesService(context);
                var autoresHasLibrosService = this.AutoresHasLibrosService(context);
                var respuestaCreacionEditorial = editorialesService.Create(editorial).GetAwaiter().GetResult();
                editorialIdCreado = editorial.Id;
                var respuestaCreacionAutor = autoresService.Create(autor).GetAwaiter().GetResult();
                autorIdCreado = autor.Id;
                libro.EditorialesId = editorialIdCreado;
                var respuestaCreacionLibro = librosService.Create(libro).GetAwaiter().GetResult();
                libroIsbnCreado = libro.Isbn;
                var respuesta = autoresHasLibrosService.Create(new AutoresHasLibros { AutoresId = autorIdCreado, LibrosIsbn = libroIsbnCreado.Value }).GetAwaiter().GetResult();
                Assert.AreEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test2AsignarAutorLibroRepetido()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresHasLibrosService = this.AutoresHasLibrosService(context);
                var respuesta = autoresHasLibrosService.Create(new AutoresHasLibros { AutoresId = autorIdCreado, LibrosIsbn = libroIsbnCreado.Value }).GetAwaiter().GetResult();
                Assert.AreNotEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test3ObtenerAutoresPorISBNLibro()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresHasLibrosService = this.AutoresHasLibrosService(context);
                var autores = autoresHasLibrosService.GetAutoresByISBNLibro(libroIsbnCreado.Value).GetAwaiter().GetResult();
                Assert.AreEqual(autores.Count, 1);
            }
        }
        [Test]
        public void Test4BorrarAsignacionAutorLibro()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresHasLibrosService = this.AutoresHasLibrosService(context);
                var respuesta = autoresHasLibrosService.Delete(new AutoresHasLibros { AutoresId = autorIdCreado, LibrosIsbn = libroIsbnCreado.Value }).GetAwaiter().GetResult();
                Assert.IsTrue(respuesta);
            }
        }
        [Test]
        public void Test5ObtenerAutoresPorISBNLibroDespuesBorrado()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresHasLibrosService = this.AutoresHasLibrosService(context);
                var autores = autoresHasLibrosService.GetAutoresByISBNLibro(libroIsbnCreado.Value).GetAwaiter().GetResult();
                Assert.AreEqual(autores.Count, 0);
            }
        }
        private AutoresHasLibrosService AutoresHasLibrosService(ApplicationDbContext context)
        {
            IAutoresHasLibrosRepository autoresHasLibrosRepository = new AutoresHasLibrosRepository(context);
            return new AutoresHasLibrosService(autoresHasLibrosRepository);
        }
        private LibrosService LibrosService(ApplicationDbContext context)
        {
            ILibrosRepository librosRepositorio = new LibrosRepository(context);
            return new LibrosService(librosRepositorio);
        }
        private EditorialesService EditorialesService(ApplicationDbContext context)
        {
            IEditorialesRepository editorialesRepositorio = new EditorialesRepository(context);
            return new EditorialesService(editorialesRepositorio);
        }
        private AutoresService AutoresService(ApplicationDbContext context)
        {
            IAutoresRepository autoresRepositorio = new AutoresRepository(context);
            return new AutoresService(autoresRepositorio);
        }
    }
}