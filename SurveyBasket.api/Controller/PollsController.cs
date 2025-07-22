


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.api.Contracts.Polls;

namespace SurveyBasket.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        public  PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GETAll()
        {
            var polls = await _pollService.GetAllAsync();
            var response = polls.Adapt<IEnumerable<PollResponse>> ();
            return Ok(response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var poll =await _pollService.GetAsync(id);
            if (poll is null)
            {
                return NotFound();
            }
            var response = poll.Adapt<PollResponse>();
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] PollRequest request,CancellationToken cancellationToken)
        {


            var newpoll = await _pollService.AddAsync(request.Adapt<Poll>());

            return CreatedAtAction(nameof(Get), new { id = newpoll.Id }, newpoll);

        }


        [HttpPut("({id})")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var isUpdate = await _pollService.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);

            if (!isUpdate)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isDelete =await _pollService.DeleteAsync(id, cancellationToken);

            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpPut("({id}/togglePublish)")]
        public async Task<IActionResult> TogglePublishStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isUpdate = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

            if (!isUpdate)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
