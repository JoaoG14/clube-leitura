using System;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class MenuPrincipal
    {
        private RepositorioAmigo repositorioAmigo;
        private TelaAmigo telaAmigo;

        private RepositorioCaixa repositorioCaixa;
        private TelaCaixa telaCaixa;

        private RepositorioRevista repositorioRevista;
        private TelaRevista telaRevista;

        private RepositorioEmprestimo repositorioEmprestimo;
        private TelaEmprestimo telaEmprestimo;

        public MenuPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            telaAmigo = new TelaAmigo(repositorioAmigo);

            repositorioCaixa = new RepositorioCaixa();
            telaCaixa = new TelaCaixa(repositorioCaixa);

            repositorioRevista = new RepositorioRevista();
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);

            repositorioEmprestimo = new RepositorioEmprestimo();
            telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);

            ConfigurarRelacionamentos();
        }

        private void ConfigurarRelacionamentos()
        {
            telaCaixa.ConfigurarRepositorioRevista(repositorioRevista);
            telaAmigo.ConfigurarRepositorioEmprestimo(repositorioEmprestimo);
            telaRevista.ConfigurarRepositorioEmprestimo(repositorioEmprestimo);
        }

        public void ExecutarAplicacao()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Clube de Leitura ===");
                Console.WriteLine("1 - Amigos");
                Console.WriteLine("2 - Caixas");
                Console.WriteLine("3 - Revistas");
                Console.WriteLine("4 - Empréstimos");
                Console.WriteLine("5 - Sair");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        telaAmigo.Menu();
                        break;
                    case "2":
                        telaCaixa.Menu();
                        break;
                    case "3":
                        telaRevista.Menu();
                        break;
                    case "4":
                        telaEmprestimo.Menu();
                        break;
                    case "5":
                        Console.WriteLine("Aplicação encerrada.");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
} 