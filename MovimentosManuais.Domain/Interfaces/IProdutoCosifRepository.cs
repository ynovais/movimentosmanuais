using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;

namespace MovimentosManuais.Domain.Interfaces;

public interface IProdutoCosifRepository
{
    Task<IEnumerable<ProdutoCosifDto>> GetAllAsync();
    Task<ProdutoCosifDto> GetByIdAsync(string codProduto, string codCosif);    
    Task DeleteAsync(string codProduto, string codCosif);
    Task AddAsync(ProdutoCosif produtoCosif);
    Task UpdateAsync(ProdutoCosif produtoCosif);
    Task<IEnumerable<ProdutoCosifDto>> GetBySpecificationAsync(ISpecification<ProdutoCosif> specification);
    Task<ProdutoCosifDto> GetSingleBySpecificationAsync(ISpecification<ProdutoCosif> specification);
}