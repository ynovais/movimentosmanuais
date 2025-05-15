using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Queries;

public class MovimentoManualQuery
{
    public ISpecification<Domain.Entities.MovimentoManual> Specification { get; set; }
}

public class MovimentoManualQueryHandler : IQueryHandler<MovimentoManualQuery, IEnumerable<MovimentoManualDto>>
{
    private readonly IMovimentoManualRepository _repository;

    public MovimentoManualQueryHandler(IMovimentoManualRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MovimentoManualDto>> HandleAsync(MovimentoManualQuery query)
    {
        return await _repository.GetBySpecificationAsync(query.Specification);
    }
}