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
    public class LibrosController : Controller
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly ILibrosService librosService;
        private readonly IEditorialesService editorialesService;

        public LibrosController(ILogger<LibrosController> logger, ILibrosService librosService, IEditorialesService editorialesService)
        {
            _logger = logger;
            this.librosService = librosService;
            this.editorialesService = editorialesService;
        }
        public async Task<IActionResult> Index()
        {
            var libros = await this.librosService.GetAll();
            if (libros.Count == 0)
            {
                ModelState.AddModelError("libros","No hay registros para mostrar");
            }
            return View(libros);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubmit(Libros model)
        {
            if (model.EditorialesId > 0)
            {
                ViewBag.EditorialesList = await this.DevolverEditorialesList(model.EditorialesId.ToString());
            }
            else
            {
                ViewBag.EditorialesList = await this.DevolverEditorialesList();
            }
            if (!this.ModelState.IsValid)
            {
                return View("Create", model);
            }
            var respuestaCreate = await this.librosService.Create(model);
            ViewBag.MensajeExito = string.Empty;
            if (respuestaCreate != string.Empty)
            {
                ModelState.AddModelError(nameof(Libros.Isbn), respuestaCreate);
            }
            else
            {
                ViewBag.MensajeExito = "Registro Insertado Correctamente";
            }
            return View("Create", model);
        }
        public async Task<SelectList> DevolverEditorialesList(string valorSeleccionado = "")
        {
            var itemsLista = new List<SelectListItem>
            {
                new SelectListItem { Selected = false, Text = "Seleccione...", Value = "-1"}
            };
            itemsLista.AddRange((await this.editorialesService.GetAll()).Select(edit => new SelectListItem { Selected = false, Text = edit.Nombre + " - " + edit.Sede, Value = edit.Id.ToString() }));
            var editorialesList = new SelectList(itemsLista, "Value", "Text", valorSeleccionado);
            return editorialesList;
        }
        public async Task<ActionResult> Create()
        {
            ViewBag.EditorialesList = await this.DevolverEditorialesList();
            return View("Create");
        }
        public async Task<ActionResult> Delete(long isbn)
        {
            if (await this.librosService.Delete(new Libros { Isbn = isbn }))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
