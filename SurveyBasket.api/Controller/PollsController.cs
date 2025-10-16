


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.api.Abstractions;
using SurveyBasket.api.Contracts.Polls;

namespace SurveyBasket.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        public  PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet]
 
        public async Task<IActionResult> GETAll()
        {
            var polls = await _pollService.GetAllAsync();
            var response = polls.Adapt<IEnumerable<PollResponse>> ();
            return Ok(response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken)
        {
            var result =await _pollService.GetAsync(id, cancellationToken);

            return Ok(result.IsSuccess? Ok(result.Value) : NotFound(result.Error));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] PollRequest request,
        CancellationToken cancellationToken)
        {
            var result = await _pollService.AddAsync(request, cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.id }, result.Value) : result.ToProblem(StatusCodes.Status400BadRequest);
        }


        [HttpPut("({id})")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(StatusCodes.Status409Conflict);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeleteAsync(id, cancellationToken);

        
            return result.IsSuccess ? NoContent() : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.code, detail: result.Error.Description);
        }


        [HttpPut("({id}/togglePublish)")]
        public async Task<IActionResult> TogglePublishStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem(StatusCodes.Status400BadRequest);

        }



    }
}
