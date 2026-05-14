using System.ComponentModel;
using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;

namespace DiseñosFlores.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo) 
        {
            _repo = repo;
        }

        public List<Cliente> Listar(string nroDocumento = "")
        {
            return _repo.GetAll(nroDocumento);
        }

        public Cliente Obtener(int id)
        {
            return _repo.GetById(id);
        }

        public void Crear(Cliente cli)
        {
            _repo.Add(cli);
        }

        public void Editar(Cliente cli)
        {
            _repo.Update(cli);
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }
    }
}
