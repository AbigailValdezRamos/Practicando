using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;

namespace DiseñosFlores.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _repo;

        public DepartamentoService(IDepartamentoRepository repo)
        {
            _repo = repo;
        }

        public List<Departamento> Listar() 
        {
          return  _repo.GetAll();
        }

        public Departamento Obtener(int id)
        {
          return  _repo.GetById(id);
        }

        public void Crear(Departamento dep)
        {
            if (string.IsNullOrEmpty(dep.NombreDepartamento))
                throw new Exception("El nombre es obligatorio");

            _repo.Add(dep);
        }

        public void Editar(Departamento dep)
        {
            _repo.Update(dep);
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }
    }
}
