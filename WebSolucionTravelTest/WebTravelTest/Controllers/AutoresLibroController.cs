using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Models;

namespace WebTravelTest.Controllers
{
    public class AutoresLibroController : Controller
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly IAutoresHasLibrosService autoresHasLibrosService;
        private readonly IAutoresService autoresService;
        private readonly ILibrosService librosService;
        public AutoresLibroController(ILogger<LibrosController> logger, IAutoresHasLibrosService autoresHasLibrosService, IAutoresService autoresService, ILibrosService librosService)
        {
            _logger = logger;
            this.autoresHasLibrosService = autoresHasLibrosService;
            this.autoresService = autoresService;
            this.librosService = librosService;
        }
        public async Task<IActionResult> Index(long isbn)
        {
            var libro = await this.librosService.GetByISBN(isbn);
            if (libro?.AutoresHasLibros?.Count == 0)
            {
                ModelState.AddModelError("autoresLibro","No hay registros para mostrar");
            }
            return View(libro);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubmit(AutoresHasLibros model)
        {
            if (model.AutoresId > 0)
            {
                ViewBag.AutoresList = await this.DevolverAutoresList(model.AutoresId.ToString());
            }
            else
            {
                ViewBag.AutoresList = await this.DevolverAutoresList();
            }
            if (!this.ModelState.IsValid)
            {
                return View("Create", model);
            }
            var respuestaCreate = await this.autoresHasLibrosService.Create(model);
            ViewBag.MensajeExito = string.Empty;
            if (respuestaCreate != string.Empty)
            {
                ModelState.AddModelError(nameof(AutoresHasLibros.AutoresId), respuestaCreate);
            }
            else
            {
                ViewBag.MensajeExito = "Registro Insertado Correctamente";
            }
            return View("Create", model);
        }
        public async Task<ActionResult> Create(long isbn)
        {
            ViewBag.AutoresList = await this.DevolverAutoresList();
            return View("Create", new AutoresHasLibros { LibrosIsbn = isbn });
        }
        public async Task<SelectList> DevolverAutoresList(string valorSeleccionado = "")
        {
            var itemsLista = new List<SelectListItem>
            {
                new SelectListItem { Selected = false, Text = "Seleccione...", Value = "-1"}
            };
            itemsLista.AddRange((await this.autoresService.GetAll()).Select(aut => new SelectListItem { Selected = false, Text = aut.Nombre + " - " + aut.Apellidos, Value = aut.Id.ToString() }));
            var autoresList = new SelectList(itemsLista, "Value", "Text", valorSeleccionado);
            return autoresList;
        }
        public async Task<ActionResult> Delete(int autor_id, long libro_ISBN)
        {
            if (await this.autoresHasLibrosService.Delete(new AutoresHasLibros { AutoresId = autor_id, LibrosIsbn = libro_ISBN }))
            {
                return RedirectToAction("Index", new { isbn = libro_ISBN });
            }
            return RedirectToAction("Index", new { isbn = libro_ISBN });
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
