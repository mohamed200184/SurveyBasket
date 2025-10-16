using Microsoft.AspNetCore.Http;

namespace SurveyBasket.api.Abstractions
{
    public record Error(string code ,string Description,int? StatusCode)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty,null);
    }
}
