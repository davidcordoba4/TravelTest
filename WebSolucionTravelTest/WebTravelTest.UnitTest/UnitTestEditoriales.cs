using NUnit.Framework;
using WebTravelTest.Entities.Models;
using WebTravelTest.Repositories;
using WebTravelTest.Repositories.Repositories;
using WebTravelTest.Core.Services;
using WebTravelTest.Repositories.Interfaces;
using WebTravelTest.Entities.DbContexts;

namespace WebTravelTest.UnitTest
{
    public class UnitTestEditoriales
    {
        // Clase Base de Datos Fake
        private SqLiteDbFake sqLiteDbFake;
        private int editorialIdCreado;
        [SetUp]
        public void Setup()
        {
            if(sqLiteDbFake == null)
            {
                sqLiteDbFake = new SqLiteDbFake();
            }
        }

        [Test]
        public void Test1CrearEditorial()
        {
            var editorial = new Editoriales() { Nombre = "Az Editora", Sede = "Buenos Aires" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var editorialesService = this.EditorialesService(context);
                var respuesta = editorialesService.Create(editorial).GetAwaiter().GetResult();
                editorialIdCreado = editorial.Id;
                Assert.AreEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test2CrearEditorialRepetido()
        {
            var editorial = new Editoriales() { Nombre = "Az Editora", Sede = "Buenos Aires" };
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var editorialesService = this.EditorialesService(context);
                var respuesta = editorialesService.Create(editorial).GetAwaiter().GetResult();
                Assert.AreNotEqual(respuesta, string.Empty);
            }
        }
        [Test]
        public void Test3ConsultarEditoriales()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var editorialesService = this.EditorialesService(context);
                var editoriales = editorialesService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(editoriales.Count, 1);
            }
        }
        [Test]
        public void Test4BorrarEditorial()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var editorialesService = this.EditorialesService(context);
                var respuesta = editorialesService.Delete(new Editoriales { Id = editorialIdCreado }).GetAwaiter().GetResult();
                Assert.IsTrue(respuesta);
            }
        }
        [Test]
        public void Test5ConsultarEditorialesDespuesBorrado()
        {
            using (var context = sqLiteDbFake.GetDbContext())
            {
                var editorialesService = this.EditorialesService(context);
                var editoriales = editorialesService.GetAll().GetAwaiter().GetResult();
                Assert.AreEqual(editoriales.Count, 0);
            }
        }
        private EditorialesService EditorialesService(ApplicationDbContext context)
        {
            IEditorialesRepository editorialesRepositorio = new EditorialesRepository(context);
            return new EditorialesService(editorialesRepositorio);
        }
    }
}