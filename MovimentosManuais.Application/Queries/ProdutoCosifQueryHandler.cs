using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Queries;

public class ProdutoCosifQuery
{
    public ISpecification<Domain.Entities.ProdutoCosif> Specification { get; set; }
}

public class ProdutoCosifQueryHandler : IQueryHandler<ProdutoCosifQuery, IEnumerable<ProdutoCosifDto>>
{
    private readonly IProdutoCosifRepository _repository;

    public ProdutoCosifQueryHandler(IProdutoCosifRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProdutoCosifDto>> HandleAsync(ProdutoCosifQuery query)
    {
        return await _repository.GetBySpecificationAsync(query.Specification);
    }
}

public class ProdutoCosifSingleQueryHandler : IQueryHandler<ProdutoCosifQuery, ProdutoCosifDto>
{
    private readonly IProdutoCosifRepository _repository;

    public ProdutoCosifSingleQueryHandler(IProdutoCosifRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProdutoCosifDto> HandleAsync(ProdutoCosifQuery query)
    {
        return await _repository.GetSingleBySpecificationAsync(query.Specification);
    }
}