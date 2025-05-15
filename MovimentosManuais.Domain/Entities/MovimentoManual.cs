using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Entities
{
    public class MovimentoManual
    {
        public int DatMes { get; set; }
        public int DatAno { get; set; }
        public long NumLancamento { get; set; }
        public string CodProduto { get; set; } = null!;
        public string CodCosif { get; set; } = null!;
        public decimal ValValor { get; set; }
        public string DesDescricao { get; set; } = null!;
        public DateTime DatMovimento { get; set; }
        public string CodUsuario { get; set; } = null!;
        public ProdutoCosif ProdutoCosif { get; set; } = null!;
    }
}
