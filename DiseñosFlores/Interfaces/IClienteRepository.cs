using DiseñosFlores.Models;
namespace DiseñosFlores.Interfaces
{
    public interface IClienteRepository
    {
   
        Cliente GetById(int id);
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
        List<Cliente> GetAll(string razonSocial = "", string direccion = "", string departamento = "");
    }
}
