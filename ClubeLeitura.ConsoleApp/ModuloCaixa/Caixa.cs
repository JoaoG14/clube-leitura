using System;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Etiqueta { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public int DiasEmprestimo { get; set; }

        public Caixa(string etiqueta, string cor, int diasEmprestimo)
        {
            Etiqueta = etiqueta ?? string.Empty;
            Cor = cor ?? string.Empty;
            DiasEmprestimo = diasEmprestimo;
        }

        public bool Validar()
        {
            bool etiquetaValida = !string.IsNullOrEmpty(Etiqueta) && Etiqueta.Length <= 50;
            bool corValida = !string.IsNullOrEmpty(Cor);
            bool diasValidos = DiasEmprestimo > 0 && DiasEmprestimo <= 7;

            return etiquetaValida && corValida && diasValidos;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Etiqueta: {Etiqueta} | Cor: {Cor} | Dias de Empréstimo: {DiasEmprestimo}";
        }
    }
} 