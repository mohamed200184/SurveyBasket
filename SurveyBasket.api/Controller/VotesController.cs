
namespace SurveyBasket.api.Controller
{
    [Route("api/polls/{pollId}/[controller]/vote")]
    [ApiController]
    public class VotesController(
        IQuestionService questionService
        ) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        [HttpGet("")]
        public async Task<IActionResult> Start([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _questionService.GetAvillableAsync(pollId, userId!, cancellationToken);
            
            if(result.IsSuccess)
                return Ok(result.Value);

            return result.Error.Equals(VoteErrors.DublicatedVote) ?
                    result.ToProblem(StatusCodes.Status409Conflict) :
                result.ToProblem(StatusCodes.Status404NotFound);

        }
    }
}
