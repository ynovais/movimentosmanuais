using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<IEnumerable<MovimentoManualDto>> GetBySpecificationAsync(ISpecification<MovimentoManual> specification);
        Task AddAsync(MovimentoManual movimento);
        Task<int> GetNextLancamentoAsync(int mes, int ano);

    }
}
