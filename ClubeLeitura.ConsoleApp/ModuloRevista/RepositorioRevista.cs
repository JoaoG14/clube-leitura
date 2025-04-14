using System;
using System.Collections.Generic;
using System.Linq;
using ClubeLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class RepositorioRevista
    {
        private List<Revista> revistas = new List<Revista>();
        private int contadorId = 1;

        public void Inserir(Revista revista)
        {
            revista.Id = contadorId++;
            revistas.Add(revista);
        }

        public void Editar(int id, Revista revistaAtualizada)
        {
            Revista? revistaSelecionada = SelecionarPorId(id);

            if (revistaSelecionada != null)
            {
                revistaSelecionada.Titulo = revistaAtualizada.Titulo;
                revistaSelecionada.NumeroEdicao = revistaAtualizada.NumeroEdicao;
                revistaSelecionada.AnoPublicacao = revistaAtualizada.AnoPublicacao;
                revistaSelecionada.Caixa = revistaAtualizada.Caixa;
            }
        }

        public void Excluir(int id)
        {
            Revista? revistaSelecionada = SelecionarPorId(id);

            if (revistaSelecionada != null)
            {
                revistas.Remove(revistaSelecionada);
            }
        }

        public Revista? SelecionarPorId(int id)
        {
            return revistas.FirstOrDefault(r => r.Id == id);
        }

        public List<Revista> SelecionarTodos()
        {
            return revistas.ToList();
        }

        public List<Revista> SelecionarRevistasPorCaixa(Caixa caixa)
        {
            return revistas.Where(r => r.Caixa.Id == caixa.Id).ToList();
        }

        public List<Revista> SelecionarRevistasDisponiveis()
        {
            return revistas.Where(r => r.Status == "Disponível").ToList();
        }

        public bool VerificarRevistasNaCaixa(int caixaId)
        {
            return revistas.Any(r => r.Caixa.Id == caixaId);
        }

        public bool VerificarTituloEdicaoDuplicados(string titulo, int numeroEdicao, int idParaIgnorar = 0)
        {
            return revistas.Any(r => r.Titulo == titulo && r.NumeroEdicao == numeroEdicao && r.Id != idParaIgnorar);
        }
    }
} 