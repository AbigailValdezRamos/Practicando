namespace DiseñosFlores.Models
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public ICollection<Cliente> Clientes { get; set; }
    }
}
