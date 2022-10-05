using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebTravelTest.Core.Interfaces;
using WebTravelTest.Entities.Models;
using WebTravelTest.Models;

namespace WebTravelTest.Controllers
{
    public class AutoresController : Controller
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly IAutoresService autoresService;
        public AutoresController(ILogger<LibrosController> logger, IAutoresService autoresService)
        {
            _logger = logger;
            this.autoresService = autoresService;
        }
        public async Task<IActionResult> Index()
        {
            var autores = await this.autoresService.GetAll();
            if (autores.Count == 0)
            {
                ModelState.AddModelError("autores","No hay registros para mostrar");
            }
            return View(autores);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubmit(Autores model)
        {
            if (!this.ModelState.IsValid)
            {
                return View("Create", model);
            }
            var respuestaCreate = await this.autoresService.Create(model);
            ViewBag.MensajeExito = string.Empty;
            if (respuestaCreate != string.Empty)
            {
                ModelState.AddModelError(nameof(Autores.Nombre), respuestaCreate);
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
            if (await this.autoresService.Delete(new Autores { Id = id }))
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
