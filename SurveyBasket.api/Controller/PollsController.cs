


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.api.Abstractions;
using SurveyBasket.api.Contracts.Polls;

namespace SurveyBasket.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        public  PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("")]
 
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            
            return Ok(await _pollService.GetAllAsync(cancellationToken));
            
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
        {

            return Ok(await _pollService.GetCurrentAsync(cancellationToken));

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

            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.id }, result.Value) : result.ToProblem();
        }


        [HttpPut("({id})")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeleteAsync(id, cancellationToken);

        
            return result.IsSuccess ? NoContent() :result.ToProblem();
        }


        [HttpPut("({id}/togglePublish)")]
        public async Task<IActionResult> TogglePublishStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }



    }
}
