using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos.ManipulaArquivos
{
    public class Read
    {
        public string CaminhoFinal { get; set; }
        public string CaminhoCadastro { get; set; }
        public string ClienteInadimplente { get; set; }
        public string CaminhoFornecedor { get; set; }
        public string CaminhoBloqueado { get; set; }

        public Read()
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
        public bool ProcurarCPFBloqueado(string cpf)
        {

            cpf = cpf.Replace(".", "").Replace("-", "");
            string cpfBloqueado = "";
          
            try
            {
                using (StreamReader sr = new StreamReader(ClienteInadimplente))
                {
                    cpfBloqueado = sr.ReadLine();

                    while (cpfBloqueado != null)
                    {
                        if (cpfBloqueado == cpf)
                        {
                            return true;
                        }
                        cpfBloqueado = sr.ReadLine();
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }

            return false;

        }
        public bool VerificaListaFornecedor()
        {
            if (File.ReadAllLines(CaminhoFornecedor).Length == 0)
                return false;
            else
                return true;
        }
        public bool VerificaListaCliente()
        {
            if (File.ReadAllLines(CaminhoCadastro).Length == 0)
                return false;
            else
                return true;
        }
        //Retorna lista de clientes
        public List<Cliente> ListaArquivoCliente()
        {
            List<Cliente> clientes = new List<Cliente>();
            string procuraCliente = "", nome = "", cpf = "";
            DateTime dNascimento, uCompra, dCadastro;
            Cliente buscaCliente;

            try
            {
                using (StreamReader sr = new StreamReader(CaminhoCadastro))
                {
                    procuraCliente = sr.ReadLine();
                    while (procuraCliente != null)
                    {
                        cpf = procuraCliente.Substring(0, 11);
                        nome = procuraCliente.Substring(11, 50);
                        dNascimento = DateTime.Parse(procuraCliente.Substring(61, 10));
                        char sexo = char.Parse(procuraCliente.Substring(71, 1));
                        uCompra = DateTime.Parse(procuraCliente.Substring(72, 10));
                        dCadastro = DateTime.Parse(procuraCliente.Substring(82, 10));
                        char situacao = char.Parse(procuraCliente.Substring(92, 1));
                        buscaCliente = new Cliente(cpf, nome, dNascimento, sexo, uCompra, dCadastro, situacao);
                        clientes.Add(buscaCliente);
                        procuraCliente = sr.ReadLine();
                    }
                }
                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return null;


        }
    }
}
