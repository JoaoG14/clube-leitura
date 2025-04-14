using System;
using ClubeLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class MenuPrincipal
    {
        private RepositorioAmigo repositorioAmigo;
        private TelaAmigo telaAmigo;

        public MenuPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            telaAmigo = new TelaAmigo(repositorioAmigo);
        }

        public void ExecutarAplicacao()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Clube de Leitura ===");
                Console.WriteLine("1 - Módulo de Amigos");
                Console.WriteLine("2 - Módulo de Caixas (Em construção)");
                Console.WriteLine("3 - Módulo de Revistas (Em construção)");
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
                        Console.WriteLine("Módulo em construção!");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("Módulo em construção!");
                        Console.ReadLine();
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