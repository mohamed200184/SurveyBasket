﻿namespace SurveyBasket.api.Contracts.Results
{
    public record VotesPerAnswerResponse
   (
        string Answer,
        int Count
    );
}
