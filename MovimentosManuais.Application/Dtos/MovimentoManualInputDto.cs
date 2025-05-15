using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Dtos
{
    public class MovimentoManualInputDto
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string CodProduto { get; set; } = null!;
        public string CodCosif { get; set; } = null!;
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = null!;
    }
}
