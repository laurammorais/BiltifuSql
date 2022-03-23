﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos.ManipularBanco
{
    public class Leitura
    {
        private const string connString = "Data Source=DESKTOP-CK90B8I;Initial Catalog=Biltiful;Integrated Security=True";

        public Cliente ProcuraCliente(string cpf)
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

        public List<Fornecedor> ListaArquivoFornecedor()
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
    }
}