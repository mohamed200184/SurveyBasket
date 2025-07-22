namespace SurveyBasket.api.Contracts.Polls
{
    public record PollResponse(
        int id,string Title,string Summary,
        bool IsPublished,
        DateOnly StartAt,
        DateOnly EndsAt

        );
   
}
