using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Web.Controllers;

[ApiController]
public abstract class BaseController<TService, TQuery, TDto, TEntity, TKey> : ControllerBase
    where TService : class
    where TQuery : class
    where TDto : class
    where TEntity : class
{
    protected readonly TService _service;
    protected readonly IQueryHandler<TQuery, IEnumerable<TDto>> _queryHandler;
    protected readonly IQueryHandler<TQuery, TDto> _singleQueryHandler;
    private readonly Func<TService, TEntity, Task> _addAsync;
    private readonly Func<TService, TEntity, Task> _updateAsync;
    private readonly Func<TService, TKey, Task> _deleteAsync;

    protected BaseController(
        TService service,
        IQueryHandler<TQuery, IEnumerable<TDto>> queryHandler,
        IQueryHandler<TQuery, TDto> singleQueryHandler,
        Func<TService, TEntity, Task> addAsync,
        Func<TService, TEntity, Task> updateAsync,
        Func<TService, TKey, Task> deleteAsync)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        _singleQueryHandler = singleQueryHandler ?? throw new ArgumentNullException(nameof(singleQueryHandler));
        _addAsync = addAsync ?? throw new ArgumentNullException(nameof(addAsync));
        _updateAsync = updateAsync ?? throw new ArgumentNullException(nameof(updateAsync));
        _deleteAsync = deleteAsync ?? throw new ArgumentNullException(nameof(deleteAsync));
    }

    [HttpGet]
    public abstract Task<ActionResult<IEnumerable<TDto>>> Get();

    [HttpGet("{id}")]
    public abstract Task<ActionResult<TDto>> Get(TKey id);

    [HttpPost]
    public virtual async Task<ActionResult> Post([FromBody] TEntity entity)
    {
        try
        {
            await _addAsync(_service, entity);
            return CreatedAtAction(nameof(Get), null, entity);
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

    [HttpPut("{id}")]
    public virtual async Task<ActionResult> Put([FromRoute] TKey id, [FromBody] TEntity entity)
    {
        try
        {
            await _updateAsync(_service, entity);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
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

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> Delete([FromRoute] TKey id)
    {
        try
        {
            await _deleteAsync(_service, id);
            return NoContent();
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
}