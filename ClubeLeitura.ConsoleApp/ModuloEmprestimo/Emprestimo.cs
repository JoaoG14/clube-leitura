using System;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoEfetiva { get; set; }
        public string Situacao { get; set; } = "Aberto";

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            DataDevolucaoPrevista = DataEmprestimo.AddDays(revista.Caixa.DiasEmprestimo);
            Situacao = "Aberto";
            
            revista.Emprestar();
        }

        public void Devolver()
        {
            DataDevolucaoEfetiva = DateTime.Now;
            Situacao = "Concluído";
            
            Revista.Devolver();
        }

        public bool EstaAtrasado()
        {
            if (Situacao == "Concluído")
                return false;
                
            return DateTime.Now > DataDevolucaoPrevista;
        }

        public TimeSpan ObterDiasAtraso()
        {
            if (!EstaAtrasado())
                return TimeSpan.Zero;

            return DateTime.Now - DataDevolucaoPrevista;
        }

        public bool Validar()
        {
            bool amigoValido = Amigo != null;
            bool revistaValida = Revista != null && Revista.Status == "Disponível";

            return amigoValido && revistaValida;
        }

        public override string ToString()
        {
            string status = Situacao;
            if (EstaAtrasado())
                status = "Atrasado";

            return $"ID: {Id} | Amigo: {Amigo.Nome} | Revista: {Revista.Titulo} | " +
                   $"Data Empréstimo: {DataEmprestimo:dd/MM/yyyy} | " +
                   $"Devolução Prevista: {DataDevolucaoPrevista:dd/MM/yyyy} | " +
                   $"Situação: {status}";
        }
    }
} 