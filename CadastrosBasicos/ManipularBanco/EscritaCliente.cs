using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos.ManipularBanco
{
    public class EscritaCliente
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public void GravarNovoCliente(Cliente cliente)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Inserir", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cliente.CPF;
                command.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                command.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = cliente.DataNascimento;
                command.Parameters.AddWithValue("@Sexo", SqlDbType.Char).Value = cliente.Sexo;
                command.Parameters.AddWithValue("@UltimaCompra", SqlDbType.Date).Value = cliente.UltimaVenda;
                command.Parameters.AddWithValue("@DataCadastro", SqlDbType.Date).Value = cliente.DataCadastro;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = cliente.Situacao;

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void EditarCliente(Cliente cliente)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Editar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cliente.CPF;
                command.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                command.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = cliente.DataNascimento;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = cliente.Situacao;

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void AlterarSituacaoDoCliente(string cpf, char situacao)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Cliente_Alterar_Situacao", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = situacao;

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}