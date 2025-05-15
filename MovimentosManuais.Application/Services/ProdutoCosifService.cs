using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Services;

public class ProdutoCosifService
{
    private readonly IProdutoCosifRepository _repository;

    public ProdutoCosifService(IProdutoCosifRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProdutoCosifDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ProdutoCosifDto> GetByIdAsync(string codProduto, string codCosif)
    {
        var produtoCosif = await _repository.GetByIdAsync(codProduto, codCosif);
        if (produtoCosif == null)
            throw new KeyNotFoundException($"ProdutoCosif de codigo {codProduto} e Codigo Cosif {codCosif} nao encontrado.");
        return produtoCosif;
    }

    public async Task AddAsync(ProdutoCosif produtoCosif)
    {
        if (produtoCosif == null)
            throw new ArgumentNullException(nameof(produtoCosif));

        if (string.IsNullOrEmpty(produtoCosif.CodProduto) || string.IsNullOrEmpty(produtoCosif.CodCosif))
            throw new ArgumentException("O Codigo do produto e o codigo cosif devem ser informados.");

        await _repository.AddAsync(produtoCosif);
    }

    public async Task UpdateAsync(ProdutoCosif produtoCosif)
    {
        if (produtoCosif == null)
            throw new ArgumentNullException(nameof(produtoCosif));

        var existing = await _repository.GetByIdAsync(produtoCosif.CodProduto, produtoCosif.CodCosif);
        if (existing == null)
            throw new KeyNotFoundException($"O produto cosif de Codigo produto {produtoCosif.CodProduto} e Codigo Cosif  {produtoCosif.CodCosif} nao encontrados.");

        await _repository.UpdateAsync(produtoCosif);
    }

    public async Task DeleteAsync(string codProduto, string codCosif)
    {
        var existing = await _repository.GetByIdAsync(codProduto, codCosif);
        if (existing == null)
            throw new KeyNotFoundException($"O Produto Cosif de codigo produto {codProduto} e Codigo Cosif {codCosif} nao encontrados.");

        await _repository.DeleteAsync(codProduto, codCosif);
    }
}