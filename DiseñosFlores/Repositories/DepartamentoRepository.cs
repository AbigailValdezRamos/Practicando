using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DiseñosFlores.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly string _connectionString;

        public DepartamentoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Departamento> GetAll() //Listar
        {
            var lista = new List<Departamento>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT IdDepartamento, NombreDepartamento FROM Departamentos";
                SqlCommand cmd = new SqlCommand(query, conn); //preparando consulta 

                conn.Open(); //abrimos conexion
                SqlDataReader reader = cmd.ExecuteReader(); //Solo lectura / memoria

                while (reader.Read()) //leemos
                {
                    lista.Add(new Departamento
                    {
                        IdDepartamento = (int)reader["IdDepartamento"],
                        NombreDepartamento = reader["NombreDepartamento"].ToString()
                    });
                }
            }

            return lista;
        }
      

        public Departamento GetById(int id)
        {
            Departamento dep = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Departamentos WHERE IdDepartamento = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    dep = new Departamento
                    {
                        IdDepartamento = (int)reader["IdDepartamento"],
                        NombreDepartamento = reader["NombreDepartamento"].ToString()
                    };
                }
            }

            return dep;
        }

        public void Add(Departamento departamento) //este metodo no retorna nada
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Departamentos (NombreDepartamento) VALUES (@nombre)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", departamento.NombreDepartamento);

                conn.Open();
                cmd.ExecuteNonQuery(); //ejecutamos
            }
        }

        public void Update(Departamento departamento)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Departamentos SET NombreDepartamento = @nombre WHERE IdDepartamento = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", departamento.NombreDepartamento);
                cmd.Parameters.AddWithValue("@id", departamento.IdDepartamento);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Departamentos WHERE IdDepartamento = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}