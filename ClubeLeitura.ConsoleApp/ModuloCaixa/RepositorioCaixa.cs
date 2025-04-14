using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa
    {
        private List<Caixa> caixas = new List<Caixa>();
        private int contadorId = 1;

        public void Inserir(Caixa caixa)
        {
            caixa.Id = contadorId++;
            caixas.Add(caixa);
        }

        public void Editar(int id, Caixa caixaAtualizada)
        {
            Caixa? caixaSelecionada = SelecionarPorId(id);

            if (caixaSelecionada != null)
            {
                caixaSelecionada.Etiqueta = caixaAtualizada.Etiqueta;
                caixaSelecionada.Cor = caixaAtualizada.Cor;
                caixaSelecionada.DiasEmprestimo = caixaAtualizada.DiasEmprestimo;
            }
        }

        public void Excluir(int id)
        {
            Caixa? caixaSelecionada = SelecionarPorId(id);

            if (caixaSelecionada != null)
            {
                caixas.Remove(caixaSelecionada);
            }
        }

        public Caixa? SelecionarPorId(int id)
        {
            return caixas.FirstOrDefault(c => c.Id == id);
        }

        public List<Caixa> SelecionarTodos()
        {
            return caixas.ToList();
        }

        public bool VerificarEtiquetaExistente(string etiqueta, int idParaIgnorar = 0)
        {
            return caixas.Any(c => c.Etiqueta == etiqueta && c.Id != idParaIgnorar);
        }
    }
} 