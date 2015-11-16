using System;

namespace WebAppCrashPerformance.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string Modelo { get; set; }
        public string Tamanho { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
