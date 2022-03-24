using System;
using System.Collections.Generic;
using CadastrosBasicos.ManipularBanco;

namespace CadastrosBasicos
{
    public class Cliente
    {
        public LeituraCliente leitura = new LeituraCliente();

        public string CPF { get; private set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Cliente() { }
        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, char situacao)
        {
            CPF = cpf;
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaVenda = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, DateTime UltimaCompra, DateTime dataCadastro, char situacao)
        {
            CPF = cpf;
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaVenda = UltimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public void BloqueiaCadastro()
        {
            Cliente cliente;
            Console.WriteLine("Insira o CPF para bloqueio: ");
            string cpf = Console.ReadLine();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (leitura.ProcurarCPFBloqueado(cpf))
            {
                bool flag = false;
                int opcao;
                Console.WriteLine("Cliente ja esta bloqueado");
                Console.WriteLine("Deseja desbloqueado ? [1 - Sim/ 2 - Nao]");

                do
                {
                    flag = int.TryParse(Console.ReadLine(), out opcao);
                } while (flag != true);

                if (opcao == 1)
                {
                    new EscritaCliente().AlterarSituacaoDoCliente(cpf, 'A');
                    Console.WriteLine("Cliente desbloqueado");
                    Console.WriteLine("Pressione enter para continuar...");
                    Console.ReadKey();
                }

            }
            else
            {
                if (Validacoes.ValidarCpf(cpf))
                {
                    cliente = leitura.ProcurarCliente(cpf);
                    if (cliente != null)
                    {
                        new EscritaCliente().AlterarSituacaoDoCliente(cliente.CPF, 'I');
                        Console.WriteLine("CPF bloqueado!");
                    }
                }
                else
                {
                    Console.WriteLine("CPF incorreto!");
                }
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public Cliente Editar()
        {
            Cliente cliente;
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Nome/Data de Nascimento/sexo/Situacao), caso nao queira alterar alguma informacao pressione enter!");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            cliente = leitura.ProcurarCliente(cpf);
            if (cliente != null)
            {
                Console.WriteLine("Nome: ");
                string nome = Console.ReadLine().Trim();
                Console.WriteLine("Data de nascimento: ");
                bool flag = DateTime.TryParse(Console.ReadLine(), out DateTime dNascimento);
                Console.WriteLine("Situacao [A - Ativo/I - Inativo]: ");
                bool flagSituacao = char.TryParse(Console.ReadLine().Trim().ToUpper(), out char situacao);

                cliente.Nome = nome == "" ? cliente.Nome : nome;
                cliente.DataNascimento = flag == false ? cliente.DataCadastro : dNascimento;
                cliente.Situacao = flagSituacao == false ? cliente.Situacao : situacao;

                new EscritaCliente().EditarCliente(cliente);

                Console.WriteLine("Cliente Cadastrado com sucesso");
                Console.WriteLine("Pressione enter para continuar...");
                Console.ReadKey();
            }
            return cliente;
        }
        public string RetornaArquivo()
        {
            return $"{CPF}{Nome}{DataNascimento.ToString("dd/MM/yyyy")}{Sexo}{UltimaVenda.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{Situacao}";

        }
        public void Navegar()
        {
            Console.WriteLine("============== Cliente ==============");
            bool verificaArquivo = leitura.ContemCliente();
            if (verificaArquivo == true)
            {
                List<Cliente> lista = leitura.TrazerClientes();
                int opcao = 0, posicao = 0;
                bool flag = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("============== Cliente ==============");

                    if (opcao == 0)
                    {
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 1)
                    {
                        if (posicao == lista.Count - 1)
                            posicao = lista.Count - 1;
                        else
                            posicao++;
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 2)
                    {
                        if (posicao == 0)
                            posicao = 0;
                        else
                            posicao--;
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 3)
                    {
                        posicao = 0;
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 4)
                    {
                        posicao = lista.Count - 1;
                        Console.WriteLine(lista[posicao].ToString());
                    }


                    Console.WriteLine(@"
1. Proximo 
2. Anterior
3. Primeiro
4. Ultimo
0. Voltar para menu anterior.
");
                    do
                    {
                        flag = int.TryParse(Console.ReadLine(), out opcao);
                    } while (flag != true);

                } while (opcao != 0);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ainda nao tem nenhum cliente cadastrado");
                Console.WriteLine("Pressione enter para continuar...");
                Console.ReadKey();
            }
        }
        public void Localizar()
        {
            Console.WriteLine("Insira o cpf para localizar: ");
            string cpf = Console.ReadLine();

            Cliente cliente = leitura.ProcurarCliente(cpf);

            if (cliente != null)
            {
                Console.WriteLine(cliente.ToString());

            }
            else
                Console.WriteLine("Nenhum cadastrado foi encontrado!");
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public void ClientesBloqueados()
        {
            Console.WriteLine("Insira o CPF para pesquisa: ");
            string cpf = Console.ReadLine();
            bool flag = new LeituraCliente().ProcurarCPFBloqueado(cpf);

            if (flag)
            {
                Cliente cliente = leitura.ProcurarCliente(cpf);
                Console.WriteLine(cliente.ToString());
            }
            else
            {
                Console.WriteLine("Cliente bloqueado nao encontrado");
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public override string ToString()
        {
            return $"CPF: {CPF}\nNome: {Nome.Trim()}\nData de nascimento: {DataNascimento.ToString("dd/MM/yyyy")}\nSexo: {Sexo}\nUltima Compra: {UltimaVenda.ToString("dd/MM/yyyy")}\nDia de Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }

    }
}
