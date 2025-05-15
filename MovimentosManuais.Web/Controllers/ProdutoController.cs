using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Queries;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;
using System.Threading.Tasks;

namespace MovimentosManuais.Web.Controllers;

[Route("api/[controller]")]
public class ProdutoController : BaseController<ProdutoService, ProdutoQuery, ProdutoDto, Produto, string>
{
    public ProdutoController(
        ProdutoService service,
        IQueryHandler<ProdutoQuery, IEnumerable<ProdutoDto>> queryHandler,
        IQueryHandler<ProdutoQuery, ProdutoDto> singleQueryHandler)
        : base(
            service,
            queryHandler,
            singleQueryHandler,
            (s, entity) => s.AddAsync(entity),
            (s, entity) => s.UpdateAsync(entity),
            (s, id) => s.DeleteAsync(id))
    {
    }

    public override async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
    {
        try
        {
            var query = new ProdutoQuery { Specification = new AllProdutosSpecification() };
            var items = await _queryHandler.HandleAsync(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    public override async Task<ActionResult<ProdutoDto>> Get(string id)
    {
        try
        {
            var query = new ProdutoQuery { Specification = new ProdutoByIdSpecification(id) };
            var item = await _singleQueryHandler.HandleAsync(query);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    public override async Task<ActionResult> Post([FromBody] Produto entity)
    {
        if (string.IsNullOrEmpty(entity.CodProduto))
            return BadRequest(new { error = "CodProduto is required." });

        return await base.Post(entity);
    }

    public override async Task<ActionResult> Put(string id, [FromBody] Produto entity)
    {
        if (id != entity.CodProduto)
            return BadRequest(new { error = "CodProduto mismatch." });

        return await base.Put(id, entity);
    }
}