using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Queries;

public class ProdutoQuery
{
    public ISpecification<Domain.Entities.Produto> Specification { get; set; }
}

public class ProdutoQueryHandler : IQueryHandler<ProdutoQuery, IEnumerable<ProdutoDto>>
{
    private readonly IProdutoRepository _repository;

    public ProdutoQueryHandler(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProdutoDto>> HandleAsync(ProdutoQuery query)
    {
        return await _repository.GetBySpecificationAsync(query.Specification);
    }
}

public class ProdutoSingleQueryHandler : IQueryHandler<ProdutoQuery, ProdutoDto>
{
    private readonly IProdutoRepository _repository;

    public ProdutoSingleQueryHandler(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProdutoDto> HandleAsync(ProdutoQuery query)
    {
        return await _repository.GetSingleBySpecificationAsync(query.Specification);
    }
}