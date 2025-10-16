namespace SurveyBasket.api.Errors
{
    public static class QuestionErrors
    {
        public static readonly Error QuestionNotFound =
            new Error("QuestionNotFound", "Question not found with given id .", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedQuestionContent =
            new Error("Question.DuplicatedQuestionContent", "another question with same content already exists.", StatusCodes.Status409Conflict);


    
}
}
