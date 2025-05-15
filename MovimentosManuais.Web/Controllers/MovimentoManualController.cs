using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Dtos;
using MovimentosManuais.Application.Queries;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Specifications;
using System;
using System.Threading.Tasks;

namespace MovimentosManuais.Web.Controllers;

[Route("api/[controller]")]
public class MovimentoManualController : BaseController<MovimentoManualService, MovimentoManualQuery, MovimentoManualDto, MovimentoManualInputDto, (int Mes, int Ano, string CodProduto, int NumLancamento)>
{
    private readonly ProdutoCosifService _produtoCosifService;

    public MovimentoManualController(
    MovimentoManualService service,
    IQueryHandler<MovimentoManualQuery, IEnumerable<MovimentoManualDto>> queryHandler,
    IQueryHandler<MovimentoManualQuery, MovimentoManualDto> singleQueryHandler,
    ProdutoCosifService produtoCosifService)
    : base(
        service,
        queryHandler,
        singleQueryHandler,
        (s, entity) => s.AddAsync(entity),
        (s, entity) => Task.CompletedTask,
        (s, key) => Task.CompletedTask)
    {
        _produtoCosifService = produtoCosifService ?? throw new ArgumentNullException(nameof(produtoCosifService));
    }

    public override async Task<ActionResult<IEnumerable<MovimentoManualDto>>> Get()
    {
        try
        {
            var query = new MovimentoManualQuery { Specification = new AllMovimentoManuaisSpecification() };
            var items = await _queryHandler.HandleAsync(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpGet("{mes}/{ano}/{codProduto}/{numLancamento}")]
    public override Task<ActionResult<MovimentoManualDto>> Get([FromRoute] (int Mes, int Ano, string CodProduto, int NumLancamento) id)
    {
        return Task.FromResult<ActionResult<MovimentoManualDto>>(NotFound(new { error = "Single item retrieval not supported." }));
    }

    [HttpGet("byDate")]
    public async Task<ActionResult<IEnumerable<MovimentoManualDto>>> GetByDate([FromQuery] int? mes, [FromQuery] int? ano)
    {
        try
        {
            var query = new MovimentoManualQuery { Specification = new MovimentoManualByDateRangeSpecification(mes, ano) };
            var items = await _queryHandler.HandleAsync(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpPost]
    public override async Task<ActionResult> Post([FromBody] MovimentoManualInputDto entity)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(Get), null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpPut("{mes}/{ano}/{codProduto}/{numLancamento}")]
    public override Task<ActionResult> Put([FromRoute] (int Mes, int Ano, string CodProduto, int NumLancamento) id, [FromBody] MovimentoManualInputDto entity)
    {
        return Task.FromResult<ActionResult>(NotFound(new { error = "Update not supported." }));
    }

    [HttpDelete("{mes}/{ano}/{codProduto}/{numLancamento}")]
    public override Task<ActionResult> Delete([FromRoute] (int Mes, int Ano, string CodProduto, int NumLancamento) id)
    {
        return Task.FromResult<ActionResult>(NotFound(new { error = "Delete not supported." }));
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<ProdutoCosifDto>>> GetProdutos()
    {
        try
        {
            var query = new ProdutoCosifQuery { Specification = new AllProdutoCosifsSpecification() };
            var produtos = await _produtoCosifService.GetAllAsync();
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }
}