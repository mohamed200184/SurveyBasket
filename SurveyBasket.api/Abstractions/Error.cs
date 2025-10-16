namespace SurveyBasket.api.Abstractions
{
    public record Error(string code ,string Description)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty);
    }
}
