namespace SurveyBasket.api.Errors
{
    public class PollErrors
    {
        public static readonly Error PollNotFound =
            new("Poll.NotFound", "No poll was found with the provided id.",StatusCodes.Status404NotFound);


        public static readonly Error DublicatedPollTitle =
            new("Poll.DublicatedPollTitle", "Anotheer Poll is with the same title.", StatusCodes.Status409Conflict);

    }
}
