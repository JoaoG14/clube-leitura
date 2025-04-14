using System;
using ClubeLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class Revista
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int NumeroEdicao { get; set; }
        public int AnoPublicacao { get; set; }
        public string Status { get; set; } = "Disponível";
        public Caixa Caixa { get; set; }

        public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
        {
            Titulo = titulo ?? string.Empty;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicacao;
            Status = "Disponível";
            Caixa = caixa;
        }

        public bool Validar()
        {
            bool tituloValido = !string.IsNullOrEmpty(Titulo) && Titulo.Length >= 2 && Titulo.Length <= 100;
            bool numeroEdicaoValido = NumeroEdicao > 0;
            bool anoPublicacaoValido = AnoPublicacao > 0 && AnoPublicacao <= DateTime.Now.Year;
            bool caixaValida = Caixa != null;
            bool statusValido = Status == "Disponível" || Status == "Emprestada" || Status == "Reservada";

            return tituloValido && numeroEdicaoValido && anoPublicacaoValido && caixaValida && statusValido;
        }

        public void Emprestar()
        {
            Status = "Emprestada";
        }

        public void Devolver()
        {
            Status = "Disponível";
        }

        public void Reservar()
        {
            Status = "Reservada";
        }

        public override string ToString()
        {
            return $"ID: {Id} | Título: {Titulo} | Edição: {NumeroEdicao} | Ano: {AnoPublicacao} | Status: {Status} | Caixa: {Caixa.Etiqueta}";
        }
    }
} 