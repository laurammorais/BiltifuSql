using System;
using System.Collections.Generic;
using System.IO;
using CadastrosBasicos.ManipularBanco;

namespace CadastrosBasicos.ManipulaArquivos
{
    public class Write
    {
        public string CaminhoFinal { get; set; }
        public string CaminhoCadastro { get; set; }
        public string ClienteInadimplente { get; set; }
        public string CaminhoFornecedor { get; set; }
        public string CaminhoBloqueado { get; set; }

        public Write()
        {
            AcharArquivos();
        }

        public void AcharArquivos()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            CaminhoFinal = Path.Combine(caminhoInicial, "DataBase");
            if (!File.Exists(CaminhoFinal))
            {
                Directory.CreateDirectory(CaminhoFinal);
            }
            CaminhoCadastro = CaminhoFinal + "\\Cliente.dat";
            if (!File.Exists(CaminhoCadastro))
                File.Create(CaminhoCadastro).Close();
            ClienteInadimplente = CaminhoFinal + "\\Risco.dat";
            if (!File.Exists(ClienteInadimplente))
                File.Create(ClienteInadimplente).Close();
            CaminhoBloqueado = CaminhoFinal + "\\Bloqueado.dat";
            if (!File.Exists(CaminhoBloqueado))
                File.Create(CaminhoBloqueado).Close();
            CaminhoFornecedor = CaminhoFinal + "\\Fornecedor.dat";
            if (!File.Exists(CaminhoFornecedor))
                File.Create(CaminhoFornecedor).Close();
        }
        public void DesbloqueiaCliente(string cpf)
        {
            List<string> cpfs = new List<string>();
            try
            {
                using (StreamReader sw = new StreamReader(ClienteInadimplente))
                {
                    string values = sw.ReadLine();
                    while (values != null)
                    {
                        if (cpf != values)
                            cpfs.Add(values);
                        values = sw.ReadLine();
                    }
                }
                File.Delete(ClienteInadimplente);
                using (StreamWriter sw = new StreamWriter(ClienteInadimplente))
                {
                    cpfs.ForEach(s => sw.WriteLine(s));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        public void BloqueiaCliente(string cpf)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(ClienteInadimplente, append: true))
                {
                    sw.WriteLine(cpf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        public void EditarFornecedor(Fornecedor fornecedorAtualizado)
        {
            List<Fornecedor> fornecedores = new Leitura().TrazerFornecedores();
            int posicao = 0;
            try
            {
                while (fornecedores[posicao] != null)
                {
                    if (fornecedorAtualizado.CNPJ == fornecedores[posicao].CNPJ)
                    {
                        fornecedores[posicao] = fornecedorAtualizado;
                        break;
                    }
                    posicao++;
                }
                File.Delete(CaminhoFornecedor);
                using (StreamWriter sw = new StreamWriter(CaminhoFornecedor))
                {
                    posicao = 0;
                    do
                    {
                        sw.WriteLine(fornecedores[posicao].RetornaArquivo());
                        posicao++;
                    } while (posicao < fornecedores.Count);
                        Console.WriteLine("Registro atualizado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }


    }
}