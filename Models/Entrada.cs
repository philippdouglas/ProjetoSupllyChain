using System;

public class Entrada
{
    public int Id { get; set; }
    public int MercadoriaId { get; set; }
    public DateTime DataHora { get; set; }
    public string Local { get; set; }
    public int Quantidade { get; set; }
    
    // Campos adicionais da tabela Mercadoria
    public string Nome { get; set; }
    public string NumeroRegistro { get; set; }
    public string Fabricante { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }

    public Mercadoria Mercadoria { get; set; }
}

