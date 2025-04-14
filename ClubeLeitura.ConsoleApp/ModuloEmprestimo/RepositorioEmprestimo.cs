using System;
using System.Collections.Generic;
using System.Linq;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo
    {
        private List<Emprestimo> emprestimos = new List<Emprestimo>();
        private int contadorId = 1;

        public void Inserir(Emprestimo emprestimo)
        {
            emprestimo.Id = contadorId++;
            emprestimos.Add(emprestimo);
        }

        public void Editar(int id, Emprestimo emprestimoAtualizado)
        {
            Emprestimo? emprestimoSelecionado = SelecionarPorId(id);

            if (emprestimoSelecionado != null)
            {
                emprestimoSelecionado.Amigo = emprestimoAtualizado.Amigo;
                emprestimoSelecionado.Revista = emprestimoAtualizado.Revista;
                emprestimoSelecionado.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoSelecionado.DataDevolucaoPrevista = emprestimoAtualizado.DataDevolucaoPrevista;
                emprestimoSelecionado.DataDevolucaoEfetiva = emprestimoAtualizado.DataDevolucaoEfetiva;
                emprestimoSelecionado.Situacao = emprestimoAtualizado.Situacao;
            }
        }

        public void Excluir(int id)
        {
            Emprestimo? emprestimoSelecionado = SelecionarPorId(id);

            if (emprestimoSelecionado != null)
            {
                emprestimos.Remove(emprestimoSelecionado);
            }
        }

        public Emprestimo? SelecionarPorId(int id)
        {
            return emprestimos.FirstOrDefault(e => e.Id == id);
        }

        public List<Emprestimo> SelecionarTodos()
        {
            return emprestimos.ToList();
        }

        public List<Emprestimo> SelecionarEmprestimosAbertos()
        {
            return emprestimos.Where(e => e.Situacao == "Aberto").ToList();
        }

        public List<Emprestimo> SelecionarEmprestimosEmAtraso()
        {
            return emprestimos.Where(e => e.EstaAtrasado()).ToList();
        }

        public List<Emprestimo> SelecionarEmprestimosPorAmigo(Amigo amigo)
        {
            return emprestimos.Where(e => e.Amigo.Id == amigo.Id).ToList();
        }

        public bool VerificarAmigoComEmprestimoEmAberto(Amigo amigo)
        {
            return emprestimos.Any(e => e.Amigo.Id == amigo.Id && e.Situacao == "Aberto");
        }

        public bool VerificarEmprestimosPorAmigo(int amigoId)
        {
            return emprestimos.Any(e => e.Amigo.Id == amigoId);
        }

        public bool VerificarEmprestimosPorRevista(int revistaId)
        {
            return emprestimos.Any(e => e.Revista.Id == revistaId && e.Situacao == "Aberto");
        }
    }
} 