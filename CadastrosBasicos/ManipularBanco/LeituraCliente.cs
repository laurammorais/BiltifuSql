using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CadastrosBasicos.ManipularBanco
{
    public class LeituraCliente
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public Cliente ProcurarCliente(string cpf)
        {
            try
            {
                Cliente cliente = new Cliente();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Buscar_Pelo_CPF", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), char.Parse(reader.GetString(3)), reader.GetDateTime(4), reader.GetDateTime(5), char.Parse(reader.GetString(6)));
                }

                connection.Close();

                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool ProcurarCPFBloqueado(string cpf)
        {
            try
            {
                Cliente cliente = new Cliente();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Buscar_Pelo_CPF_Bloqueado", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), char.Parse(reader.GetString(3)), reader.GetDateTime(3), reader.GetDateTime(4), char.Parse(reader.GetString(5)));
                }

                connection.Close();

                return cliente.Situacao == 'I' || cliente.Situacao == 'i';
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public List<Cliente> TrazerClientes()
        {
            try
            {
                List<Cliente> cliente = new List<Cliente>();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Buscar", connection);
                command.CommandType = CommandType.StoredProcedure;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cliente.Add(new Cliente(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), char.Parse(reader.GetString(3)), reader.GetDateTime(4), reader.GetDateTime(5), char.Parse(reader.GetString(6))));
                }

                connection.Close();

                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool ContemCliente()
        {
            List<Cliente> clientes = TrazerClientes();
            return clientes.Any();
        }
    }
}