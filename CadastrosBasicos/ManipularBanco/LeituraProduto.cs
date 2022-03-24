using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos.ManipularBanco
{
    public class LeituraProduto
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public Produto Buscar(string cod)
        {
            try
            {
                Produto produto = new Produto();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Produto_Buscar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodigoBarras", SqlDbType.NVarChar).Value = cod;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    produto = new Produto(reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDateTime(3), reader.GetDateTime(4), char.Parse(reader.GetString(5)));
                } 

                connection.Close();

                return produto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


    }
}