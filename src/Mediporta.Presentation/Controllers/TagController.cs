using MediatR;
using Mediporta.Application.Commands;
using Mediporta.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Mediporta.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly IMediator _mediator;

    public TagController(ILogger<TagController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetTags")]
    public async Task<IActionResult> GetTags(
        [FromQuery] int page,
        [FromQuery] int pagesize,
        [FromQuery] string sort,
        [FromQuery] string order)
    {
        var request = new GetTagsQuery(page, pagesize, sort, order);
        var result = await _mediator.Send(request);

        return result.Match<IActionResult>(
            tags => Ok(tags),
            error =>
            {
                _logger.LogError(error.Message);
                return BadRequest(error.Message);
            });
    }

    [HttpPost(Name = "FetchTags")]
    public async Task<IActionResult> FetchTags()
    {
        try
        {
            var request = new RecreateDatabaseCommand();
            await _mediator.Send(request);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        
    }
}

