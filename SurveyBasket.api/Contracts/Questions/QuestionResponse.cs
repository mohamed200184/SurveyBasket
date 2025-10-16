using SurveyBasket.api.Contracts.Answers;

namespace SurveyBasket.api.Contracts.Questions
{
    public record QuestionResponse
    (
       int Id,
       string Content,
     IEnumerable<AnswerResponse> Answers
     );
}
