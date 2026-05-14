using System.Runtime.Intrinsics.Arm;
using DiseñosFlores.Interfaces;
using DiseñosFlores.Models;
using Microsoft.Data.SqlClient;

namespace DiseñosFlores.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository (string connectionString)

        {
            _connectionString = connectionString;
        }

        public List<Cliente> GetAll(string razonSocial = "")
        {
            var lista = new List<Cliente>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            SELECT c.IdCliente,
                   c.TipoDocumento,
                   c.NroDocumento,
                   c.RazonSocial,
                   c.Direccion,
                   c.Telefono,
                   c.IdDepartamento,
                   d.NombreDepartamento
            FROM Clientes c
            INNER JOIN Departamentos d
                ON c.IdDepartamento = d.IdDepartamento
            WHERE ( c.RazonSocial LIKE '%' + @razonSocial + '%') ";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@razonSocial", razonSocial);

                conn.Open();   

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Cliente()
                    {
                        IdCliente = (int)reader["IdCliente"],
                        TipoDocumento = reader["TipoDocumento"].ToString(),
                        NroDocumento = reader["NroDocumento"].ToString(),
                        RazonSocial = reader["RazonSocial"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        IdDepartamento = (int)reader["IdDepartamento"],

                        Departamento = new Departamento()
                        {
                            NombreDepartamento = reader["NombreDepartamento"].ToString()
                        }
                    });
                }
            }

            return lista;
        }
        public Cliente GetById(int id)
        {
            Cliente cli = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                        SELECT c.*, d.NombreDepartamento
                        FROM Clientes c
                        INNER JOIN Departamentos d
                            ON c.IdDepartamento = d.IdDepartamento
                        WHERE c.IdCliente = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cli = new Cliente
                    {
                        IdCliente = (int)reader["IdCliente"],
                        TipoDocumento = reader["TipoDocumento"].ToString(),
                        NroDocumento = reader["NroDocumento"].ToString(),
                        RazonSocial = reader["RazonSocial"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        IdDepartamento = (int)reader["IdDepartamento"],

                        Departamento = new Departamento
                        {
                            NombreDepartamento = reader["NombreDepartamento"].ToString()
                        }
                    };
                }
            }

            return cli;
        }


        public void Add(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO Clientes
                    (TipoDocumento, NroDocumento, RazonSocial,
                     Direccion, Telefono, IdDepartamento)

                    VALUES
                    (@tipo, @nrodoc, @razon,
                     @direccion, @telefono, @iddep)";

                SqlCommand cmd = new SqlCommand(query, conn);

         
                cmd.Parameters.AddWithValue("@tipo", cliente.TipoDocumento ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nrodoc", cliente.NroDocumento ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@razon", cliente.RazonSocial );
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@iddep", cliente.IdDepartamento);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Update(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                string query = @"
                    UPDATE Clientes
                    SET TipoDocumento = @tipo,
                        NroDocumento = @nrodoc,
                        RazonSocial = @razon,
                        Direccion = @direccion,
                        Telefono = @telefono,
                        IdDepartamento = @iddep
                    WHERE IdCliente = @id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@tipo", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@nrodoc", cliente.NroDocumento);
                cmd.Parameters.AddWithValue("@razon", cliente.RazonSocial);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@iddep", cliente.IdDepartamento);
                cmd.Parameters.AddWithValue("@id", cliente.IdCliente);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Clientes WHERE IdCliente = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
