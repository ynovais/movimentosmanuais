using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Queries;

public class MovimentoManualSingleQueryHandler : IQueryHandler<MovimentoManualQuery, MovimentoManualDto>
{
    private readonly IMovimentoManualRepository _repository;

    public MovimentoManualSingleQueryHandler(IMovimentoManualRepository repository)
    {
        _repository = repository;
    }

    public async Task<MovimentoManualDto> HandleAsync(MovimentoManualQuery query)
    {
        var results = await _repository.GetBySpecificationAsync(query.Specification);
        return results.FirstOrDefault();
    }
}