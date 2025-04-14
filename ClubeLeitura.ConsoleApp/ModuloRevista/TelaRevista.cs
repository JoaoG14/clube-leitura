using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class TelaRevista
    {
        private RepositorioRevista repositorioRevista;
        private RepositorioCaixa repositorioCaixa;
        private RepositorioEmprestimo repositorioEmprestimo;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }

        public void ConfigurarRepositorioEmprestimo(RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Módulo de Revistas ===");
                Console.WriteLine("1 - Inserir nova revista");
                Console.WriteLine("2 - Editar revista");
                Console.WriteLine("3 - Excluir revista");
                Console.WriteLine("4 - Visualizar todas as revistas");
                Console.WriteLine("5 - Visualizar revistas por caixa");
                Console.WriteLine("6 - Voltar ao menu principal");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        Inserir();
                        break;
                    case "2":
                        Editar();
                        break;
                    case "3":
                        Excluir();
                        break;
                    case "4":
                        VisualizarTodos();
                        break;
                    case "5":
                        VisualizarRevistasPorCaixa();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Inserir Nova Revista ===");

            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada. Cadastre uma caixa primeiro.");
                Console.ReadLine();
                return;
            }

            Revista novaRevista = ObterDadosRevista(caixas);

            if (novaRevista == null || !novaRevista.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioRevista.VerificarTituloEdicaoDuplicados(novaRevista.Titulo, novaRevista.NumeroEdicao))
            {
                Console.WriteLine("Já existe uma revista com esse título e edição!");
                Console.ReadLine();
                return;
            }

            repositorioRevista.Inserir(novaRevista);
            Console.WriteLine("Revista inserida com sucesso!");
            Console.ReadLine();
        }

        private void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Editar Revista ===");

            if (repositorioRevista.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhuma revista cadastrada!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID da revista que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Revista? revistaSelecionada = repositorioRevista.SelecionarPorId(id);

            if (revistaSelecionada == null)
            {
                Console.WriteLine("Revista não encontrada!");
                Console.ReadLine();
                return;
            }

            if (revistaSelecionada.Status == "Emprestada")
            {
                Console.WriteLine("Não é possível editar uma revista emprestada!");
                Console.ReadLine();
                return;
            }

            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            Revista revistaAtualizada = ObterDadosRevista(caixas);

            if (revistaAtualizada == null || !revistaAtualizada.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioRevista.VerificarTituloEdicaoDuplicados(revistaAtualizada.Titulo, revistaAtualizada.NumeroEdicao, id))
            {
                Console.WriteLine("Já existe uma revista com esse título e edição!");
                Console.ReadLine();
                return;
            }

            repositorioRevista.Editar(id, revistaAtualizada);
            Console.WriteLine("Revista editada com sucesso!");
            Console.ReadLine();
        }

        private void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Revista ===");

            if (repositorioRevista.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhuma revista cadastrada!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID da revista que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Revista? revistaSelecionada = repositorioRevista.SelecionarPorId(id);

            if (revistaSelecionada == null)
            {
                Console.WriteLine("Revista não encontrada!");
                Console.ReadLine();
                return;
            }

            if (repositorioEmprestimo != null && repositorioEmprestimo.VerificarEmprestimosPorRevista(id))
            {
                Console.WriteLine("Não é possível excluir uma revista com empréstimos em aberto!");
                Console.ReadLine();
                return;
            }

            repositorioRevista.Excluir(id);
            Console.WriteLine("Revista excluída com sucesso!");
            Console.ReadLine();
        }

        private void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Revistas ===");

            List<Revista> revistas = repositorioRevista.SelecionarTodos();

            if (revistas.Count == 0)
            {
                Console.WriteLine("Nenhuma revista cadastrada!");
            }
            else
            {
                foreach (Revista revista in revistas)
                {
                    Console.WriteLine(revista);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void VisualizarRevistasPorCaixa()
        {
            Console.Clear();
            Console.WriteLine("=== Revistas por Caixa ===");

            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Caixas disponíveis:");
            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine(caixa);
            }

            Console.Write("\nDigite o ID da caixa para ver suas revistas: ");
            if (!int.TryParse(Console.ReadLine(), out int caixaId))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Caixa? caixaSelecionada = repositorioCaixa.SelecionarPorId(caixaId);

            if (caixaSelecionada == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                Console.ReadLine();
                return;
            }

            List<Revista> revistas = repositorioRevista.SelecionarRevistasPorCaixa(caixaSelecionada);

            Console.Clear();
            Console.WriteLine($"=== Revistas na Caixa: {caixaSelecionada.Etiqueta} ===");

            if (revistas.Count == 0)
            {
                Console.WriteLine("Nenhuma revista nesta caixa!");
            }
            else
            {
                foreach (Revista revista in revistas)
                {
                    Console.WriteLine(revista);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private Revista ObterDadosRevista(List<Caixa> caixas)
        {
            Console.Write("Título (2-100 caracteres): ");
            string titulo = Console.ReadLine() ?? string.Empty;

            Console.Write("Número da Edição (número positivo): ");
            int.TryParse(Console.ReadLine(), out int numeroEdicao);

            Console.Write("Ano de Publicação: ");
            int.TryParse(Console.ReadLine(), out int anoPublicacao);

            Console.WriteLine("\nCaixas Disponíveis:");
            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine(caixa);
            }

            Console.Write("\nDigite o ID da caixa para a revista: ");
            if (!int.TryParse(Console.ReadLine(), out int caixaId))
            {
                Console.WriteLine("ID de caixa inválido!");
                return null;
            }

            Caixa? caixaSelecionada = repositorioCaixa.SelecionarPorId(caixaId);

            if (caixaSelecionada == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                return null;
            }

            return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
        }
    }
} 