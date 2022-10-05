using NUnit.Framework;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories;
using WebTravelTest.Repositories.Repositories;
using WebTravelTest.Core.Services;
using WebTravelTest.Repositories.Interfaces;
using WebTravelTest.Entities.DbContexts;

namespace WebTravelTest.UnitTest
{
    public class UnitTestLibros
    {
        // Clase Base de Datos Fake
        private SqLiteDbFake sqLiteDbFake;
        private int editorialIdCreado;
        private long? libroIsbnCreado;
        [SetUp]
        public void Setup()
        {
            if(sqLiteDbFake == null)
            {
                sqLiteDbFake = new SqLiteDbFake();
            }
        }

        [Test]
        public void Test1CrearLibro()
        {
            var editorial = new Editoriales() { Nombre = "Az Editora", Sede = "Buenos Aires" };
            var libro = new Libros() { Isbn = 5465465465465, Titulo = "Un viaje por el conocimiento humano", Sinopsis = "Sinopsis un viaje por el conocimiento humano", NPaginas = "2145567" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var editorialesService = this.EditorialesService(context);
                var respuestaCreacionEditorial = editorialesService.Create(editorial).GetAwaiter().GetResult();
                editorialIdCreado = editorial.Id;
                libro.EditorialesId = editorialIdCreado;
                var respuesta = librosService.Create(libro).GetAwaiter().GetResult();
                libroIsbnCreado = libro.Isbn;
                Assert.AreEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test2CrearLibroRepetido()
        {
            var libro = new Libros() { Isbn = 5465465465465, Titulo = "Un viaje por el conocimiento humano", Sinopsis = "Sinopsis un viaje por el conocimiento humano", NPaginas = "2145567" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                libro.EditorialesId = editorialIdCreado;
                var respuesta = librosService.Create(libro).GetAwaiter().GetResult();
                Assert.AreNotEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test3ConsultarLibros()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var libros = librosService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(libros.Count, 1);
            }
        }
        [Test]
        public void Test4ConsultarLibroPorISBN()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var libro = librosService.GetByISBN(libroIsbnCreado??0).GetAwaiter().GetResult();
                Assert.IsNotNull(libro);
            }
        }
        [Test]
        public void Test5ConsultarEditorialPorISBN()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var editorial = librosService.GetEditorialByISBN(libroIsbnCreado ?? 0).GetAwaiter().GetResult();
                Assert.IsNotNull(editorial);
            }
        }
        [Test]
        public void Test6BorrarLibro()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var respuesta = librosService.Delete(new Libros { Isbn = libroIsbnCreado }).GetAwaiter().GetResult();
                Assert.IsTrue(respuesta);
            }
        }
        [Test]
        public void Test7ConsultarLibrosDespuesBorrado()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var librosService = this.LibrosService(context);
                var libros = librosService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(libros.Count, 0);
            }
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
    }
}