using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<ProdutoDto>> GetAllAsync();
    Task<ProdutoDto> GetByIdAsync(string codProduto);
    Task AddAsync(Produto produto);
    Task UpdateAsync(Produto produto);
    Task DeleteAsync(string codProduto);
    Task<IEnumerable<ProdutoDto>> GetBySpecificationAsync(ISpecification<Produto> specification);
    Task<ProdutoDto> GetSingleBySpecificationAsync(ISpecification<Produto> specification);
}