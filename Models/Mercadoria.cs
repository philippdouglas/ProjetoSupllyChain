using System.Collections.Generic;
using System.Linq;

public class Mercadoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NumeroRegistro { get; set; }
    public string Fabricante { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }

    public ICollection<Entrada> Entradas { get; set; }
    public ICollection<Saida> Saidas { get; set; }

    private int _estoque; // Campo privado para armazenar o valor do estoque

    // Propriedade "Estoque" que retorna o valor do estoque considerando entradas e saídas
    public int Estoque
    {
        get
        {
            int totalEntradas = Entradas != null && Entradas.Any() ? Entradas.Sum(entrada => entrada.Quantidade) : 0;
            int totalSaidas = Saidas != null && Saidas.Any() ? Saidas.Sum(saida => saida.Quantidade) : 0;
            return totalEntradas - totalSaidas; // Subtrai as saídas das entradas
        }
        set
        {
            _estoque = value; // Define o valor do estoque no campo privado
        }
    }
}

