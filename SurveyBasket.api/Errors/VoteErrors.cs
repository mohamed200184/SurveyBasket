namespace SurveyBasket.api.Errors
{
    public class VoteErrors
    {


        public static readonly Error DublicatedVote =
            new("Voted.DublicatedVote", "This user already voted before for this poll.");

    }
}
