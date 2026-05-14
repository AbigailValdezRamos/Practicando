using DiseñosFlores.Models;

namespace DiseñosFlores.Interfaces
{
    public interface IDepartamentoService
    {
        List<Departamento> Listar();
        Departamento Obtener(int id);
        void Crear(Departamento dep);
        void Editar(Departamento dep);
        void Eliminar(int id);
    }
}
