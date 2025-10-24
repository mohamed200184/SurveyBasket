namespace SurveyBasket.api.Contracts.Results
{
    public record VotesPerDayResponse
    (
        DateOnly Date ,
        int NumberOfVotes
        );
    
}
