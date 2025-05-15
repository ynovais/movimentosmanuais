using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ProdutoDto> GetByIdAsync(string codProduto)
    {
        var produto = await _repository.GetByIdAsync(codProduto);
        if (produto == null)
            throw new KeyNotFoundException($"Produto with CodProduto {codProduto} not found.");
        return produto;
    }

    public async Task AddAsync(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        if (string.IsNullOrEmpty(produto.CodProduto))
            throw new ArgumentException("CodProduto is required.");

        await _repository.AddAsync(produto);
    }

    public async Task UpdateAsync(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        var existing = await _repository.GetByIdAsync(produto.CodProduto);
        if (existing == null)
            throw new KeyNotFoundException($"Produto with CodProduto {produto.CodProduto} not found.");

        await _repository.UpdateAsync(produto);
    }

    public async Task DeleteAsync(string codProduto)
    {
        var existing = await _repository.GetByIdAsync(codProduto);
        if (existing == null)
            throw new KeyNotFoundException($"Produto with CodProduto {codProduto} not found.");

        await _repository.DeleteAsync(codProduto);
    }
}