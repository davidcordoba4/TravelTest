using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Models;

namespace WebTravelTest.Controllers
{
    public class EditorialesController : Controller
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly IEditorialesService editorialesService;
        public EditorialesController(ILogger<LibrosController> logger, IEditorialesService editorialesService)
        {
            _logger = logger;
            this.editorialesService = editorialesService;
        }
        public async Task<IActionResult> Index()
        {
            var editoriales = await this.editorialesService.GetAll();
            if (editoriales.Count == 0)
            {
                ModelState.AddModelError("editoriales","No hay registros para mostrar");
            }
            return View(editoriales);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubmit(Editoriales model)
        {
            if (!this.ModelState.IsValid)
            {
                return View("Create", model);
            }
            var respuestaCreate = await this.editorialesService.Create(model);
            ViewBag.MensajeExito = string.Empty;
            if (respuestaCreate != string.Empty)
            {
                ModelState.AddModelError(nameof(Editoriales.Nombre), respuestaCreate);
            }
            else
            {
                ViewBag.MensajeExito = "Registro Insertado Correctamente";
            }
            return View("Create", model);
        }
        public ActionResult Create()
        {
            return View("Create");
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (await this.editorialesService.Delete(new Editoriales { Id = id }))
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
