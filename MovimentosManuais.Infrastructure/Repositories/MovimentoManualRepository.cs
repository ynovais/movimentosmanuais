using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Domain.Specifications;
using MovimentosManuais.Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovimentosManuais.Infrastructure.Repositories;

public class MovimentoManualRepository : IMovimentoManualRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    public MovimentoManualRepository(ApplicationDbContext context)
    {
        _context = context;
        _connectionString = context.Database.GetConnectionString();
    }

    public async Task<IEnumerable<MovimentoManualDto>> GetBySpecificationAsync(ISpecification<MovimentoManual> specification)
    {
        var result = new List<MovimentoManualDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("spListaMovimentoManual", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new MovimentoManualDto
                        {
                            Mes = reader.GetInt32("Mes"),
                            Ano = reader.GetInt32("Ano"),
                            CodigoProduto = reader.GetString("CodigoProduto"),
                            DescricaoProduto = reader.IsDBNull("DescricaoProduto") ? null : reader.GetString("DescricaoProduto"),
                            NumeroLancamento = reader.GetInt64("NumeroLancamento"),
                            Descricao = reader.GetString("Descricao"),
                            Valor = reader.GetDecimal("Valor")
                        });
                    }
                }
            }
        }

        
        return result;
    }

    public async Task AddAsync(MovimentoManual movimento)
    {
        await _context.MovimentosManuais.AddAsync(movimento);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetNextLancamentoAsync(int mes, int ano)
    {
        var maxLancamento = await _context.MovimentosManuais
            .Where(mm => mm.DatMes == mes && mm.DatAno == ano)
            .MaxAsync(mm => (int?)mm.NumLancamento) ?? 0;
        return maxLancamento + 1;
    }
}