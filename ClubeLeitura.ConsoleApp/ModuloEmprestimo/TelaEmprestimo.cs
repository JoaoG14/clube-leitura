using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;

        public TelaEmprestimo(
            RepositorioEmprestimo repositorioEmprestimo,
            RepositorioAmigo repositorioAmigo,
            RepositorioRevista repositorioRevista)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Módulo de Empréstimos ===");
                Console.WriteLine("1 - Registrar empréstimo");
                Console.WriteLine("2 - Registrar devolução");
                Console.WriteLine("3 - Visualizar todos os empréstimos");
                Console.WriteLine("4 - Visualizar empréstimos em aberto");
                Console.WriteLine("5 - Visualizar empréstimos em atraso");
                Console.WriteLine("6 - Visualizar empréstimos por amigo");
                Console.WriteLine("7 - Voltar ao menu principal");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        RegistrarEmprestimo();
                        break;
                    case "2":
                        RegistrarDevolucao();
                        break;
                    case "3":
                        VisualizarTodos();
                        break;
                    case "4":
                        VisualizarEmprestimosEmAberto();
                        break;
                    case "5":
                        VisualizarEmprestimosEmAtraso();
                        break;
                    case "6":
                        VisualizarEmprestimosPorAmigo();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void RegistrarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Empréstimo ===");

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado. Cadastre um amigo primeiro.");
                Console.ReadLine();
                return;
            }

            List<Revista> revistasDisponiveis = repositorioRevista.SelecionarRevistasDisponiveis();
            if (revistasDisponiveis.Count == 0)
            {
                Console.WriteLine("Nenhuma revista disponível para empréstimo.");
                Console.ReadLine();
                return;
            }

            Amigo? amigoSelecionado = ObterAmigo(amigos);
            if (amigoSelecionado == null)
                return;

            if (repositorioEmprestimo.VerificarAmigoComEmprestimoEmAberto(amigoSelecionado))
            {
                Console.WriteLine("Este amigo já possui um empréstimo em aberto.");
                Console.ReadLine();
                return;
            }

            Revista? revistaSelecionada = ObterRevista(revistasDisponiveis);
            if (revistaSelecionada == null)
                return;

            Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);

            repositorioEmprestimo.Inserir(novoEmprestimo);
            Console.WriteLine("Empréstimo registrado com sucesso!");
            Console.WriteLine($"Data de devolução prevista: {novoEmprestimo.DataDevolucaoPrevista:dd/MM/yyyy}");
            Console.ReadLine();
        }

        private void RegistrarDevolucao()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Devolução ===");

            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarEmprestimosAbertos();
            if (emprestimosAbertos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo em aberto.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Empréstimos em aberto:");
            foreach (Emprestimo emprestimo in emprestimosAbertos)
            {
                Console.WriteLine(emprestimo);
            }

            Console.Write("\nDigite o ID do empréstimo que deseja devolver: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Emprestimo? emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(id);

            if (emprestimoSelecionado == null)
            {
                Console.WriteLine("Empréstimo não encontrado!");
                Console.ReadLine();
                return;
            }

            if (emprestimoSelecionado.Situacao == "Concluído")
            {
                Console.WriteLine("Este empréstimo já foi devolvido!");
                Console.ReadLine();
                return;
            }

            emprestimoSelecionado.Devolver();
            Console.WriteLine("Devolução registrada com sucesso!");

            if (emprestimoSelecionado.EstaAtrasado())
            {
                TimeSpan diasAtraso = emprestimoSelecionado.ObterDiasAtraso();
                Console.WriteLine($"Atenção: Devolução atrasada em {Math.Ceiling(diasAtraso.TotalDays)} dias!");
            }

            Console.ReadLine();
        }

        private void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Empréstimos ===");

            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

            if (emprestimos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo registrado!");
            }
            else
            {
                foreach (Emprestimo emprestimo in emprestimos)
                {
                    Console.WriteLine(emprestimo);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void VisualizarEmprestimosEmAberto()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos em Aberto ===");

            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarEmprestimosAbertos();

            if (emprestimosAbertos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo em aberto!");
            }
            else
            {
                foreach (Emprestimo emprestimo in emprestimosAbertos)
                {
                    Console.WriteLine(emprestimo);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void VisualizarEmprestimosEmAtraso()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos em Atraso ===");

            List<Emprestimo> emprestimosEmAtraso = repositorioEmprestimo.SelecionarEmprestimosEmAtraso();

            if (emprestimosEmAtraso.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo em atraso!");
            }
            else
            {
                foreach (Emprestimo emprestimo in emprestimosEmAtraso)
                {
                    TimeSpan atraso = emprestimo.ObterDiasAtraso();
                    Console.WriteLine($"{emprestimo} - Dias de atraso: {Math.Ceiling(atraso.TotalDays)}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void VisualizarEmprestimosPorAmigo()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos por Amigo ===");

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Amigos disponíveis:");
            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine(amigo);
            }

            Console.Write("\nDigite o ID do amigo para ver seus empréstimos: ");
            if (!int.TryParse(Console.ReadLine(), out int amigoId))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Amigo? amigoSelecionado = repositorioAmigo.SelecionarPorId(amigoId);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return;
            }

            List<Emprestimo> emprestimosDoAmigo = repositorioEmprestimo.SelecionarEmprestimosPorAmigo(amigoSelecionado);

            Console.Clear();
            Console.WriteLine($"=== Empréstimos do Amigo: {amigoSelecionado.Nome} ===");

            if (emprestimosDoAmigo.Count == 0)
            {
                Console.WriteLine("Este amigo não possui empréstimos registrados!");
            }
            else
            {
                foreach (Emprestimo emprestimo in emprestimosDoAmigo)
                {
                    Console.WriteLine(emprestimo);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private Amigo? ObterAmigo(List<Amigo> amigos)
        {
            Console.WriteLine("Amigos disponíveis:");
            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine(amigo);
            }

            Console.Write("\nDigite o ID do amigo: ");
            if (!int.TryParse(Console.ReadLine(), out int amigoId))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return null;
            }

            Amigo? amigoSelecionado = repositorioAmigo.SelecionarPorId(amigoId);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return null;
            }

            return amigoSelecionado;
        }

        private Revista? ObterRevista(List<Revista> revistas)
        {
            Console.WriteLine("\nRevistas disponíveis:");
            foreach (Revista revista in revistas)
            {
                Console.WriteLine(revista);
            }

            Console.Write("\nDigite o ID da revista: ");
            if (!int.TryParse(Console.ReadLine(), out int revistaId))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return null;
            }

            Revista? revistaSelecionada = repositorioRevista.SelecionarPorId(revistaId);

            if (revistaSelecionada == null || revistaSelecionada.Status != "Disponível")
            {
                Console.WriteLine("Revista não encontrada ou não disponível!");
                Console.ReadLine();
                return null;
            }

            return revistaSelecionada;
        }
    }
} 