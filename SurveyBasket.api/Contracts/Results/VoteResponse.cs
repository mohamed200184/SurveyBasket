using System.Data;

namespace SurveyBasket.api.Contracts.Results
{
    public record VoteResponse
   ( 
        string VoterName,
        DateTime VotedDate,
        IEnumerable<QuestionAnswerResponse> SelectedAnswers
    );
}
