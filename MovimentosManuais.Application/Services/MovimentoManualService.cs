using MovimentosManuais.Application.Dtos;
using MovimentosManuais.Application.Queries;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Services;

public class MovimentoManualService
{
    private readonly IQueryHandler<MovimentoManualQuery, IEnumerable<MovimentoManualDto>> _queryHandler;
    private readonly IMovimentoManualRepository _repository;

    public MovimentoManualService(
        IQueryHandler<MovimentoManualQuery, IEnumerable<MovimentoManualDto>> queryHandler,
        IMovimentoManualRepository repository)
    {
        _queryHandler = queryHandler;
        _repository = repository;
    }

    public async Task<IEnumerable<MovimentoManualDto>> GetAllAsync()
    {
        var query = new MovimentoManualQuery { Specification = new AllMovimentoManuaisSpecification() };
        return await _queryHandler.HandleAsync(query);
    }

    public async Task<IEnumerable<MovimentoManualDto>> GetByDateRangeAsync(int? mes, int? ano)
    {
        var query = new MovimentoManualQuery { Specification = new MovimentoManualByDateRangeSpecification(mes, ano) };
        return await _queryHandler.HandleAsync(query);
    }

    public async Task AddAsync(MovimentoManualInputDto input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        if (input.Mes < 1 || input.Mes > 12)
            throw new ArgumentException("Mes must be between 1 and 12.");
        if (input.Ano < 1900 || input.Ano > DateTime.Now.Year)
            throw new ArgumentException("Ano is invalid.");
        if (string.IsNullOrEmpty(input.CodProduto))
            throw new ArgumentException("CodProduto is required.");
        if (string.IsNullOrEmpty(input.CodCosif))
            throw new ArgumentException("CodCosif is required.");
        if (input.Valor <= 0)
            throw new ArgumentException("ValValor must be greater than 0.");

        var movimento = new MovimentoManual
        {
            DatMes = input.Mes,
            DatAno = input.Ano,
            CodProduto = input.CodProduto,
            CodCosif = input.CodCosif,
            NumLancamento = await _repository.GetNextLancamentoAsync(input.Mes, input.Ano),
            DesDescricao = input.Descricao,
            ValValor = input.Valor,
            CodUsuario =  "SYSTEM",
            DatMovimento = DateTime.Now
        };

        await _repository.AddAsync(movimento);
    }

    [Obsolete("Use GetAllAsync instead.")]
    public async Task<IEnumerable<MovimentoManualDto>> GetMovimentosAsync()
    {
        return await GetAllAsync();
    }

    [Obsolete("Use AddAsync instead.")]
    public async Task AddMovimentoAsync(MovimentoManualInputDto input)
    {
        await AddAsync(input);
    }
}