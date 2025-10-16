namespace SurveyBasket.api.Contracts.Votes
{
    public record VoteRequest
    (
        IEnumerable<VoteAnswerRequest> Answers
        );
}
