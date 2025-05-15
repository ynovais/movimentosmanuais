using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Entities
{
    public class ProdutoCosif
    {
        public string CodProduto { get; set; } = null!;
        public string CodCosif { get; set; } = null!;
        public string? CodClassificacao { get; set; }
        public string? StaStatus { get; set; }
        public Produto Produto { get; set; } = null!;
        public ICollection<MovimentoManual> MovimentosManuais { get; set; } = new List<MovimentoManual>();
    }
}
