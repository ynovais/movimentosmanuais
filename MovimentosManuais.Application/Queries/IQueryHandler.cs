using System.Threading.Tasks;

namespace MovimentosManuais.Application.Queries;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}