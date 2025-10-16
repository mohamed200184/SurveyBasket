namespace SurveyBasket.api.Contracts.Questions
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty()
                .NotNull()
                .Length(3,1000);
            RuleFor(x=> x.Answers).Must(x => x.Count > 1)
                .WithMessage("Questions should has at least 2 answers")
              .When(x=>x.Answers != null) ;



            RuleFor(x => x.Answers)
                .NotNull();

            RuleFor(x => x.Answers)
                .NotNull()
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("You cannot add duplicted answers for the same question");
        }
    }
}
