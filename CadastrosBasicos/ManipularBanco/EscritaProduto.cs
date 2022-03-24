using System;
using System.Data;
using System.Data.SqlClient;
namespace CadastrosBasicos.ManipularBanco
{
    public class EscritaProduto
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public void GravarProduto(Produto produto)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Produto_Inserir", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodigoBarras", SqlDbType.NVarChar).Value = produto.CodigoBarras;
                command.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = produto.Nome;
                command.Parameters.AddWithValue("@ValorVenda", SqlDbType.Decimal).Value = produto.ValorVenda;
                command.Parameters.AddWithValue("UltimaVenda", SqlDbType.Date).Value = produto.UltimaVenda;
                command.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = produto.Situacao;

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