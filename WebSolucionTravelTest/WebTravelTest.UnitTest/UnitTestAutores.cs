using NUnit.Framework;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories;
using WebTravelTest.Repositories.Repositories;
using WebTravelTest.Core.Services;
using WebTravelTest.Repositories.Interfaces;
using WebTravelTest.Entities.DbContexts;

namespace WebTravelTest.UnitTest
{
    public class UnitTestAutores
    {
        // Clase Base de Datos Fake
        private SqLiteDbFake sqLiteDbFake;
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
        public void Test1CrearAutor()
        {
            var autor = new Autores() { Nombre = "Carlos", Apellidos = "Cardona" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var respuesta = autoresService.Create(autor).GetAwaiter().GetResult();
                autorIdCreado = autor.Id;
                Assert.AreEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test2CrearAutorRepetido()
        {
            var autor = new Autores() { Nombre = "Carlos", Apellidos = "Cardona" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var respuesta = autoresService.Create(autor).GetAwaiter().GetResult();
                Assert.AreNotEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test3ConsultarAutores()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var autores = autoresService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(autores.Count, 1);
            }
        }
        [Test]
        public void Test4ConsultarAutorId()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var autor = autoresService.GetById(autorIdCreado).GetAwaiter().GetResult();
                Assert.IsNotNull(autor);
            }
        }
        [Test]
        public void Test5BorrarAutor()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var respuesta = autoresService.Delete(new Autores { Id = autorIdCreado }).GetAwaiter().GetResult();
                Assert.IsTrue(respuesta);
            }
        }
        [Test]
        public void Test6ConsultarAutoresDespuesBorrado()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var autoresService = this.AutoresService(context);
                var autores = autoresService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(autores.Count, 0);
            }
        }
        private AutoresService AutoresService(ApplicationDbContext context)
        {
            IAutoresRepository autoresRepositorio = new AutoresRepository(context);
            return new AutoresService(autoresRepositorio);
        }
    }
}