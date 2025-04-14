using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.ModuloRevista;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCaixa
    {
        private RepositorioCaixa repositorioCaixa;
        private RepositorioRevista repositorioRevista;

        public TelaCaixa(RepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        public void ConfigurarRepositorioRevista(RepositorioRevista repositorioRevista)
        {
            this.repositorioRevista = repositorioRevista;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Módulo de Caixas ===");
                Console.WriteLine("1 - Inserir nova caixa");
                Console.WriteLine("2 - Editar caixa");
                Console.WriteLine("3 - Excluir caixa");
                Console.WriteLine("4 - Visualizar todas as caixas");
                Console.WriteLine("5 - Voltar ao menu principal");
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
            Console.WriteLine("=== Inserir Nova Caixa ===");

            Caixa novaCaixa = ObterDadosCaixa();

            if (!novaCaixa.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioCaixa.VerificarEtiquetaExistente(novaCaixa.Etiqueta))
            {
                Console.WriteLine("Já existe uma caixa com essa etiqueta!");
                Console.ReadLine();
                return;
            }

            repositorioCaixa.Inserir(novaCaixa);
            Console.WriteLine("Caixa inserida com sucesso!");
            Console.ReadLine();
        }

        private void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Editar Caixa ===");

            if (repositorioCaixa.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID da caixa que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Caixa? caixaSelecionada = repositorioCaixa.SelecionarPorId(id);

            if (caixaSelecionada == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                Console.ReadLine();
                return;
            }

            Caixa caixaAtualizada = ObterDadosCaixa();

            if (!caixaAtualizada.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioCaixa.VerificarEtiquetaExistente(caixaAtualizada.Etiqueta, id))
            {
                Console.WriteLine("Já existe uma caixa com essa etiqueta!");
                Console.ReadLine();
                return;
            }

            repositorioCaixa.Editar(id, caixaAtualizada);
            Console.WriteLine("Caixa editada com sucesso!");
            Console.ReadLine();
        }

        private void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Caixa ===");

            if (repositorioCaixa.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID da caixa que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Caixa? caixaSelecionada = repositorioCaixa.SelecionarPorId(id);

            if (caixaSelecionada == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                Console.ReadLine();
                return;
            }

            if (repositorioRevista != null && repositorioRevista.VerificarRevistasNaCaixa(id))
            {
                Console.WriteLine("Não é possível excluir uma caixa que possui revistas vinculadas!");
                Console.ReadLine();
                return;
            }

            repositorioCaixa.Excluir(id);
            Console.WriteLine("Caixa excluída com sucesso!");
            Console.ReadLine();
        }

        private void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Caixas ===");

            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
            }
            else
            {
                foreach (Caixa caixa in caixas)
                {
                    Console.WriteLine(caixa);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private Caixa ObterDadosCaixa()
        {
            Console.Write("Etiqueta (texto único, máximo 50 caracteres): ");
            string etiqueta = Console.ReadLine() ?? string.Empty;

            Console.Write("Cor (hexadecimal ou paleta): ");
            string cor = Console.ReadLine() ?? string.Empty;

            Console.Write("Dias de Empréstimo (1-7): ");
            int.TryParse(Console.ReadLine(), out int diasEmprestimo);

            return new Caixa(etiqueta, cor, diasEmprestimo);
        }
    }
} 