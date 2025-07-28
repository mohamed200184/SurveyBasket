namespace SurveyBasket.api.Contracts.Polls
{
    public class PollRequestValidator : AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty()//.WithMessage("please add a {PropertyName} ")
                .Length(3,100)
               // .WithMessage("Title should be at least and maximum,youu enterde [{PropertyValue}]")
                ;

            RuleFor(x => x.Summary).NotEmpty()
               .Length(3, 1500);

            RuleFor(x => x.StartAt).NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.EndsAt).NotEmpty();
            RuleFor(x => x)
                .Must(HasValidDates)
                .WithName(nameof(PollRequest.EndsAt))
                .WithMessage("{PropertyName} must be greater than or equels start data  ");


        }

        private bool HasValidDates(PollRequest pollRequest)
        {
            return pollRequest.EndsAt >=pollRequest.StartAt;//true
        }
    }
}
