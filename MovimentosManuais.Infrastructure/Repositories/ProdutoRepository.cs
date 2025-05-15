using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using MovimentosManuais.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovimentosManuais.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
    {
        return await _context.Produtos
            .Select(p => new ProdutoDto
            {
                CodProduto = p.CodProduto,
                DesProduto = p.DesProduto
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProdutoDto> GetByIdAsync(string codProduto)
    {
        return await _context.Produtos
            .Where(p => p.CodProduto == codProduto)
            .Select(p => new ProdutoDto
            {
                CodProduto = p.CodProduto,
                DesProduto = p.DesProduto
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string codProduto)
    {
        var produto = await _context.Produtos.FindAsync(codProduto);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProdutoDto>> GetBySpecificationAsync(ISpecification<Produto> specification)
    {
        var query = ApplySpecification(specification, _context.Produtos);
        return await query
            .Select(p => new ProdutoDto
            {
                CodProduto = p.CodProduto,
                DesProduto = p.DesProduto
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProdutoDto> GetSingleBySpecificationAsync(ISpecification<Produto> specification)
    {
        var query = ApplySpecification(specification, _context.Produtos);
        return await query
            .Select(p => new ProdutoDto
            {
                CodProduto = p.CodProduto,
                DesProduto = p.DesProduto
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    private static IQueryable<T> ApplySpecification<T>(ISpecification<T> spec, IQueryable<T> query) where T : class
    {
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        foreach (var include in spec.Includes)
            query = query.Include(include);

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);
        else if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        return query;
    }
}