using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo
    {
        private List<Amigo> amigos = new List<Amigo>();
        private int contadorId = 1;

        public void Inserir(Amigo amigo)
        {
            amigo.Id = contadorId++;
            amigos.Add(amigo);
        }

        public void Editar(int id, Amigo amigoAtualizado)
        {
            Amigo? amigoSelecionado = SelecionarPorId(id);

            if (amigoSelecionado != null)
            {
                amigoSelecionado.Nome = amigoAtualizado.Nome;
                amigoSelecionado.NomeResponsavel = amigoAtualizado.NomeResponsavel;
                amigoSelecionado.Telefone = amigoAtualizado.Telefone;
            }
        }

        public void Excluir(int id)
        {
            Amigo? amigoSelecionado = SelecionarPorId(id);

            if (amigoSelecionado != null)
            {
                amigos.Remove(amigoSelecionado);
            }
        }

        public Amigo? SelecionarPorId(int id)
        {
            return amigos.FirstOrDefault(a => a.Id == id);
        }

        public List<Amigo> SelecionarTodos()
        {
            return amigos.ToList();
        }

        public bool VerificarNomeTelefoneExistente(string nome, string telefone, int idParaIgnorar = 0)
        {
            return amigos.Any(a => (a.Nome == nome || a.Telefone == telefone) && a.Id != idParaIgnorar);
        }
    }
} 