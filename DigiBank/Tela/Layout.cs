using System;
using System.Linq;
using System.Collections.Generic;
using DigiBank.Sistema;
using System.Threading;

namespace DigiBank.Tela
{
    public class Layout
    {
        private static List<Pessoa> _pessoas = new List<Pessoa>();
        private static int _opcao = 0;

        public static void TelaPrincipal()
        {

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                                                              ");
            Console.WriteLine("                  Digite a opção desejada:                    ");
            Console.WriteLine("               ================================               ");
            Console.WriteLine("                  1 - Criar conta                             ");
            Console.WriteLine("               ================================               ");
            Console.WriteLine("                  2 - Entrar com CPF e Senha                  ");
            Console.WriteLine("               ================================               ");

            _opcao = int.Parse(Console.ReadLine());

            switch (_opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                               ");
            Console.WriteLine("                   Digite seu nome:                            ");
            string nome = Console.ReadLine();
            Console.WriteLine("               ================================                ");
            Console.WriteLine("                   Digite seu CPF:                             ");
            string cpf = Console.ReadLine();
            Console.WriteLine("               ================================                ");
            Console.WriteLine("                   Digite sua senha:                           ");
            string senha = Console.ReadLine();
            Console.WriteLine("               ================================                ");

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            _pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("                   Conta cadastrada com sucesso                   ");
            Console.WriteLine("                  ================================                ");

            Thread.Sleep(2000);

            TelaContaLogada(pessoa);

        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                               ");
            Console.WriteLine("                   Digite o CPF:                               ");
            string cpf = Console.ReadLine();
            Console.WriteLine("               ================================                ");
            Console.WriteLine("                   Digite a senha:                             ");
            string senha = Console.ReadLine();

            Console.WriteLine(cpf); 
            Console.WriteLine(senha);

            Pessoa pessoa = _pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("                   Pessoa não cadastrada                             ");
                Console.WriteLine("               ================================                      ");

            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | " +
                $"Banco: {pessoa.Conta.GetCodigoDoBanco()} | " +
                $"Agência: {pessoa.Conta.GetNumeroAgencia()} | " +
                $"Conta: {pessoa.Conta.GetNumeroDaConta()}";
                  

            Console.WriteLine();
            Console.WriteLine("                ================================               ");
            Console.WriteLine($"               Seja bem vindo, {msgTelaBemVindo}              ");
            Console.WriteLine();

        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);

            Console.WriteLine("                   Digite a opção desejada:                             ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                   1 - Realizar um Depósito                             ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                   2 - Realizar um Saque                                ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                   3 - Consultar Saldo                                  ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                   4 - Extrato                                          ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                   5 - Sair                                             ");
            Console.WriteLine("               ================================                         ");

            _opcao = int.Parse(Console.ReadLine());

            switch(_opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("                   Opção inválida!                                      ");
                    Console.WriteLine("               ================================                         ");
                    break;
            }
        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine("                   Digite o valor do depósito                           ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("               ================================                         ");

            pessoa.Conta.Deposita(valor);
            
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                        ");
            Console.WriteLine("                                                                        ");
            Console.WriteLine("                Depósito realizado com sucesso                          ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                                                                        ");
            Console.WriteLine("                                                                        ");

            TelaVoltarLogado(pessoa);

        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine("                   Digite o valor do saque                              ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("               ================================                         ");

            bool saque = pessoa.Conta.Saca(valor);



            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                        ");
            Console.WriteLine("                                                                        ");

            if (saque)
            {
                Console.WriteLine("                Saque realizado com sucesso                             ");
                Console.WriteLine("               ================================                         ");
            }
            else
            {
                Console.WriteLine("                Saldo insuficiente                                      ");
                Console.WriteLine("               ================================                         ");
            }

            Console.WriteLine("                                                                        ");
            Console.WriteLine("                                                                        ");

            TelaVoltarLogado(pessoa);

        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine($"                   Seu saldo é: {pessoa.Conta.ConsultaSaldo()}        ");
            Console.WriteLine("                   =============================================       ");
            Console.WriteLine("                                                                       ");

            TelaVoltarLogado(pessoa);

        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);

            if(pessoa.Conta.Extrato().Any())
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);


                foreach(Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                                       ");
                    Console.WriteLine("                                                                       ");
                    Console.WriteLine($"                  Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"                   Tipo de movimentação: {extrato.Descricao}          ");
                    Console.WriteLine($"                   Valor: {extrato.Valor}                             ");
                    Console.WriteLine("                   =============================================       ");
                }

                Console.WriteLine("                                                                       ");
                Console.WriteLine("                                                                       ");
                Console.WriteLine($"                   SUB TOTAL: {total}                                 ");
                Console.WriteLine("                   =============================================       ");

            }
            else
            {
                Console.WriteLine($"                   Não há extrato a ser exibido!                      ");
                Console.WriteLine("                   =============================================       ");
            }

            TelaVoltarLogado(pessoa);

        }

        private static void TelaVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                Entre com uma opção abaixo:                             ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                1 - Voltar para minha conta                             ");
            Console.WriteLine("               ================================                         ");
            Console.WriteLine("                2 - Sair                                                ");
            Console.WriteLine("               ================================                         ");

            _opcao = int.Parse(Console.ReadLine());

            if (_opcao == 1)
                TelaContaLogada(pessoa);
            else
                TelaPrincipal();

        }

        private static void TelaVoltarDeslogado()
        {
            Console.WriteLine("                Entre com uma opção abaixo:                             ");
            Console.WriteLine("                =============================                           ");
            Console.WriteLine("                1 - Voltar para o menu principal                        ");
            Console.WriteLine("               =================================                        ");
            Console.WriteLine("                2 - Sair                                                ");
            Console.WriteLine("               ================================                         ");

            _opcao = int.Parse(Console.ReadLine());

            if (_opcao == 1)
                TelaPrincipal();
            else
            {
                Console.WriteLine("                Opção Inválida!                                         ");
                Console.WriteLine("                =============================                           ");
            }

        }


    }
}
