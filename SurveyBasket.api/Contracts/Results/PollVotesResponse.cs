namespace SurveyBasket.api.Contracts.Results
{
    public record PollVotesResponse
    (
        string Ttile ,
        IEnumerable<VoteResponse> Votes
        

    );
}
