using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiseñosFlores.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;
        private readonly IDepartamentoService _depService;

        public ClienteController(IClienteService service, IDepartamentoService depService)
        {
            _service = service;
            _depService = depService;
        }

        public IActionResult Index(string razonSocial = "", string direccion="" , string departamento ="")
        {
            var lista = _service.Listar(razonSocial,direccion,departamento);

            ViewBag.Departamentos = new SelectList(
                _depService.Listar(),
                "NombreDepartamento",
                "NombreDepartamento"
            );

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest") //Identifica si la solicitud fue un AJAX
            {
                return PartialView("_TablaClientes", lista);
            }

            return View(lista);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departamentos = _depService.Listar();

            ViewBag.Departamentos = new SelectList(
                departamentos,
                "IdDepartamento",
                "NombreDepartamento"
            );
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cli)
        {
            if (ModelState.IsValid)
            {
                _service.Crear(cli);
                return RedirectToAction("Index");
            }

            // 🔴 ESTO FALTABA
            ViewBag.Departamentos = new SelectList(
                _depService.Listar(),   // tu servicio de departamentos
                "IdDepartamento",
                "NombreDepartamento");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cli = _service.Obtener(id);

            var departamentos = _depService.Listar();

            ViewBag.Departamentos = new SelectList(
                departamentos,
                "IdDepartamento",
                "NombreDepartamento",
                cli.IdDepartamento
            );
            return View(cli);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cli)
        {
            _service.Editar(cli);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cli = _service.Obtener(id);
            return View(cli);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var cli = _service.Obtener(id);
            return View(cli);
        }
    }
}
