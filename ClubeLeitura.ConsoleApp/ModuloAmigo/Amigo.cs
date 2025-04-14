using System;
using System.Text.RegularExpressions;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NomeResponsavel { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome ?? string.Empty;
            NomeResponsavel = nomeResponsavel ?? string.Empty;
            Telefone = telefone ?? string.Empty;
        }

        public bool Validar()
        {
            bool nomeValido = Nome.Length >= 3 && Nome.Length <= 100;
            bool nomeResponsavelValido = NomeResponsavel.Length >= 3 && NomeResponsavel.Length <= 100;

            Regex telefoneRegex = new Regex(@"^\(\d{2}\) \d{4,5}-\d{4}$");
            bool telefoneValido = telefoneRegex.IsMatch(Telefone);

            return nomeValido && nomeResponsavelValido && telefoneValido;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | Responsável: {NomeResponsavel} | Telefone: {Telefone}";
        }
    }
} 