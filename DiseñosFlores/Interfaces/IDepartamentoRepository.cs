using DiseñosFlores.Models;

namespace DiseñosFlores.Interfaces
{
    public interface IDepartamentoRepository
    {
        List<Departamento> GetAll();
        Departamento GetById(int id);
        void Add(Departamento departamento);
        void Update(Departamento departamento);
        void Delete(int id);
    }
}
