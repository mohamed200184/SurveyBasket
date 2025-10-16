namespace SurveyBasket.api.Errors
{
    public class VoteErrors
    {


        public static readonly Error DublicatedVote =
            new("Voted.DublicatedVote", "This user already voted before for this poll.",StatusCodes.Status409Conflict);


        public static readonly Error InvalidQuestion =
       new("Voted.InvalidQuestion", "This user already voted before for this poll.", StatusCodes.Status400BadRequest);
    }
}
