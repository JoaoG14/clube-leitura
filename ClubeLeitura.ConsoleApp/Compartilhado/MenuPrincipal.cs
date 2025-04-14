using System;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloRevista;

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

        public MenuPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            telaAmigo = new TelaAmigo(repositorioAmigo);

            repositorioCaixa = new RepositorioCaixa();
            telaCaixa = new TelaCaixa(repositorioCaixa);

            repositorioRevista = new RepositorioRevista();
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);

            ConfigurarRelacionamentos();
        }

        private void ConfigurarRelacionamentos()
        {
            telaCaixa.ConfigurarRepositorioRevista(repositorioRevista);
        }

        public void ExecutarAplicacao()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Clube de Leitura ===");
                Console.WriteLine("1 - Módulo de Amigos");
                Console.WriteLine("2 - Módulo de Caixas");
                Console.WriteLine("3 - Módulo de Revistas");
                Console.WriteLine("4 - Módulo de Empréstimos (Em construção)");
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
                        Console.WriteLine("Módulo em construção!");
                        Console.ReadLine();
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