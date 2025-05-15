using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Dtos
{
    public class MovimentoManualDto
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string CodigoProduto { get; set; } = null!;
        public string DescricaoProduto { get; set; } = null!;
        public long NumeroLancamento { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
