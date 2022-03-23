using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos.ManipularBanco
{
    public class Escrita
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

        public void GravarNovoFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Fornecedor_Inserir", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = fornecedor.CNPJ;
                command.Parameters.AddWithValue("@Razao_Social", SqlDbType.NVarChar).Value = fornecedor.RazaoSocial;
                command.Parameters.AddWithValue("@Data_Abertura", SqlDbType.Date).Value = fornecedor.DataAbertura;
                command.Parameters.AddWithValue("UltimaCompra", SqlDbType.Date).Value = fornecedor.UltimaCompra;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = fornecedor.Situacao;

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void AlterarSituacaoDoFornecedor(string cnpj, char situacao)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Fornecedor_Alterar_Status", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = situacao;

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


    }
}