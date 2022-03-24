using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CadastrosBasicos.ManipularBanco
{
    public class LeituraFornecedor
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public Fornecedor ProcurarFornecedor(string cnpj)
        {
            try
            {
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                Fornecedor fornecedor = new Fornecedor();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Fornecedor_Buscar_Pelo_CNPJ", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fornecedor = new Fornecedor(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetDateTime(4), char.Parse(reader.GetString(5)));
                }

                connection.Close();

                return fornecedor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool ProcurarCNPJBloqueado(string cnpj)
        {
            try
            {
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                Fornecedor fornecedor = new Fornecedor();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Fornecedor_Buscar_Pelo_CNPJ", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fornecedor = new Fornecedor(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetDateTime(4), char.Parse(reader.GetString(5)));
                }

                connection.Close();

                return fornecedor.Situacao == 'I' || fornecedor.Situacao == 'i';
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public List<Fornecedor> TrazerFornecedores()
        {
            try
            {
                List<Fornecedor> fornecedores = new List<Fornecedor>();

                using SqlConnection connection = new SqlConnection(connString);
                connection.Open();

                using SqlCommand command = new SqlCommand("Fornecedor_Buscar", connection);
                command.CommandType = CommandType.StoredProcedure;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fornecedores.Add(new Fornecedor(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetDateTime(4), char.Parse(reader.GetString(5))));
                }

                connection.Close();

                return fornecedores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public bool ContemFornecedor()
        {
            List<Fornecedor> fornecedores = TrazerFornecedores();
            return fornecedores.Any();
        }
    }
}