using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;
using DiseñosFlores.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiseñosFlores.Controllers
{

    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _service;

        public DepartamentoController(IDepartamentoService service)
        {
            _service = service;
        
        }

        public IActionResult Index()
        {
            var lista = _service.Listar();
            
            return View(lista);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departamento dep)
        {
            //if (ModelState.IsValid)
            //{
                _service.Crear(dep);
                return RedirectToAction("Index");
            //}
            //return View(dep);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dep = _service.Obtener(id);
            return View(dep);
        }

        [HttpPost]
        public IActionResult Edit(Departamento dep)
        {
            _service.Editar(dep);
            return RedirectToAction("Index"); //redirigime a mi acccion
        }

        public IActionResult Delete(int id)
        {
            var dep = _service.Obtener(id);
            return View(dep);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var dep = _service.Obtener(id);
            return View(dep);
        }
    }
}