using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaAmigo
    {
        private RepositorioAmigo repositorioAmigo;
        private RepositorioEmprestimo repositorioEmprestimo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
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
                Console.WriteLine("=== Módulo de Amigos ===");
                Console.WriteLine("1 - Inserir novo amigo");
                Console.WriteLine("2 - Editar amigo");
                Console.WriteLine("3 - Excluir amigo");
                Console.WriteLine("4 - Visualizar todos os amigos");
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
            Console.WriteLine("=== Inserir Novo Amigo ===");

            Amigo novoAmigo = ObterDadosAmigo();

            if (!novoAmigo.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioAmigo.VerificarNomeTelefoneExistente(novoAmigo.Nome, novoAmigo.Telefone))
            {
                Console.WriteLine("Já existe um amigo com esse nome ou telefone!");
                Console.ReadLine();
                return;
            }

            repositorioAmigo.Inserir(novoAmigo);
            Console.WriteLine("Amigo inserido com sucesso!");
            Console.ReadLine();
        }

        private void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Editar Amigo ===");

            if (repositorioAmigo.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID do amigo que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Amigo? amigoSelecionado = repositorioAmigo.SelecionarPorId(id);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return;
            }

            Amigo amigoAtualizado = ObterDadosAmigo();

            if (!amigoAtualizado.Validar())
            {
                Console.WriteLine("Dados inválidos!");
                Console.ReadLine();
                return;
            }

            if (repositorioAmigo.VerificarNomeTelefoneExistente(amigoAtualizado.Nome, amigoAtualizado.Telefone, id))
            {
                Console.WriteLine("Já existe um amigo com esse nome ou telefone!");
                Console.ReadLine();
                return;
            }

            repositorioAmigo.Editar(id, amigoAtualizado);
            Console.WriteLine("Amigo editado com sucesso!");
            Console.ReadLine();
        }

        private void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Amigo ===");

            if (repositorioAmigo.SelecionarTodos().Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return;
            }

            VisualizarTodos();

            Console.Write("Digite o ID do amigo que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
                return;
            }

            Amigo? amigoSelecionado = repositorioAmigo.SelecionarPorId(id);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return;
            }

            if (repositorioEmprestimo != null && repositorioEmprestimo.VerificarEmprestimosPorAmigo(id))
            {
                Console.WriteLine("Não é possível excluir um amigo que possui empréstimos vinculados!");
                Console.ReadLine();
                return;
            }

            repositorioAmigo.Excluir(id);
            Console.WriteLine("Amigo excluído com sucesso!");
            Console.ReadLine();
        }

        private void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Amigos ===");

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
            }
            else
            {
                foreach (Amigo amigo in amigos)
                {
                    Console.WriteLine(amigo);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private Amigo ObterDadosAmigo()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Nome do Responsável: ");
            string nomeResponsavel = Console.ReadLine() ?? string.Empty;

            Console.Write("Telefone (formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX): ");
            string telefone = Console.ReadLine() ?? string.Empty;

            return new Amigo(nome, nomeResponsavel, telefone);
        }
    }
} 