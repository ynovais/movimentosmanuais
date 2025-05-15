using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Queries;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Specifications;
using System.Threading.Tasks;

namespace MovimentosManuais.Web.Controllers;

[Route("api/[controller]")]
public class ProdutoCosifController : BaseController<ProdutoCosifService, ProdutoCosifQuery, ProdutoCosifDto, ProdutoCosif, (string CodProduto, string CodCosif)>
{
    public ProdutoCosifController(
        ProdutoCosifService service,
        IQueryHandler<ProdutoCosifQuery, IEnumerable<ProdutoCosifDto>> queryHandler,
        IQueryHandler<ProdutoCosifQuery, ProdutoCosifDto> singleQueryHandler)
        : base(
            service,
            queryHandler,
            singleQueryHandler,
            (s, entity) => s.AddAsync(entity),
            (s, entity) => s.UpdateAsync(entity),
            (s, key) => s.DeleteAsync(key.CodProduto, key.CodCosif))
    {
    }

    public override async Task<ActionResult<IEnumerable<ProdutoCosifDto>>> Get()
    {
        try
        {
            var query = new ProdutoCosifQuery { Specification = new AllProdutoCosifsSpecification() };
            var items = await _queryHandler.HandleAsync(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpGet("{codProduto}/{codCosif}")]
    public override async Task<ActionResult<ProdutoCosifDto>> Get([FromRoute] (string CodProduto, string CodCosif) id)
    {
        try
        {
            var query = new ProdutoCosifQuery { Specification = new ProdutoCosifByIdSpecification(id.CodProduto, id.CodCosif) };
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

    [HttpPut("{codProduto}/{codCosif}")]
    public override async Task<ActionResult> Put([FromRoute] (string CodProduto, string CodCosif) id, [FromBody] ProdutoCosif entity)
    {
        if (id.CodProduto != entity.CodProduto || id.CodCosif != entity.CodCosif)
            return BadRequest(new { error = "CodProduto or CodCosif mismatch." });

        return await base.Put(id, entity);
    }

    [HttpDelete("{codProduto}/{codCosif}")]
    public override async Task<ActionResult> Delete([FromRoute] (string CodProduto, string CodCosif) id)
    {
        return await base.Delete(id);
    }

    [HttpGet("byProduto/{codProduto}")]
    public async Task<ActionResult<IEnumerable<ProdutoCosifDto>>> GetByProduto(string codProduto)
    {
        try
        {
            var query = new ProdutoCosifQuery { Specification = new ProdutoCosifByProdutoSpecification(codProduto) };
            var items = await _queryHandler.HandleAsync(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }
}