using DiseñosFlores.Models;

namespace DiseñosFlores.Interfaces
{
    public interface IClienteService
    {
     
        Cliente Obtener(int id);
        void Crear(Cliente cli);
        void Editar(Cliente cli);
        void Eliminar(int id);
        List<Cliente> Listar(string razonSocial = "", string direccion="" , string departamento ="");
    }
}
