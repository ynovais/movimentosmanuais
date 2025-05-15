using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using MovimentosManuais.Infrastructure.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public class ProdutoCosifRepository : IProdutoCosifRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoCosifRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProdutoCosifDto>> GetAllAsync()
    {
        return await _context.ProdutosCosif
            .Join(_context.Produtos,
                pc => pc.CodProduto,
                p => p.CodProduto,
                (pc, p) => new ProdutoCosifDto
                {
                    CodProduto = pc.CodProduto,
                    CodCosif = pc.CodCosif,
                    CodClassificacao = $"{pc.CodCosif} - {pc.CodClassificacao}"
                })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProdutoCosifDto> GetByIdAsync(string codProduto, string codCosif)
    {
        return await _context.ProdutosCosif
            .Join(_context.Produtos,
                pc => pc.CodProduto,
                p => p.CodProduto,
                (pc, p) => new ProdutoCosifDto
                {
                    CodProduto = pc.CodProduto,
                    CodCosif = pc.CodCosif,
                    CodClassificacao = $"{pc.CodCosif} - {pc.CodClassificacao}"
                })
            .AsNoTracking()
            .FirstOrDefaultAsync(pc => pc.CodProduto == codProduto && pc.CodCosif == codCosif);
    }

    public async Task AddAsync(ProdutoCosif produtoCosif)
    {
        await _context.ProdutosCosif.AddAsync(produtoCosif);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProdutoCosif produtoCosif)
    {
        _context.ProdutosCosif.Update(produtoCosif);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string codProduto, string codCosif)
    {
        var produtoCosif = await _context.ProdutosCosif 
            .FirstOrDefaultAsync(pc => pc.CodProduto == codProduto && pc.CodCosif == codCosif);
        if (produtoCosif != null)
        {
            _context.ProdutosCosif.Remove(produtoCosif);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProdutoCosifDto>> GetBySpecificationAsync(ISpecification<ProdutoCosif> specification)
    {
        var query = ApplySpecification(specification, _context.ProdutosCosif);
        return await query
            .Select(pc => new ProdutoCosifDto
            {
                CodProduto = pc.CodProduto,
                CodCosif = pc.CodCosif,
                CodClassificacao = $"{pc.CodCosif} - {pc.CodClassificacao}"
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProdutoCosifDto> GetSingleBySpecificationAsync(ISpecification<ProdutoCosif> specification)
    {
        var query = ApplySpecification(specification, _context.ProdutosCosif);
        return await query
            .Select(pc => new ProdutoCosifDto
            {
                CodProduto = pc.CodProduto,
                CodCosif = pc.CodCosif,
                CodClassificacao = $"{pc.CodCosif} - {pc.CodClassificacao}"
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