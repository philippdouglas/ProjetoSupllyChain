using System;

namespace SupplyChain.Models
{
    public class EntradaESaidaViewModel
    {
        public string TipoMovimento { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NumeroRegistro { get; set; }
        public string Fabricante { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public string Color { get; set; }
    }
}


