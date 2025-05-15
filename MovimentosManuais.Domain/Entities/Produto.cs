using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Entities
{
    public class Produto
    {
        public string CodProduto { get; set; } = null!;
        public string? DesProduto { get; set; }
        public string? StaStatus { get; set; }
        public ICollection<ProdutoCosif> ProdutosCosif { get; set; } = new List<ProdutoCosif>();
    }
}
