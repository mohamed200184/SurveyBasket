namespace SurveyBasket.api.Errors
{
    public static class QuestionErrors
    {
        public static readonly Error QuestionNotFound =
            new Error("QuestionNotFound", "Question not found with given id .");

        public static readonly Error DuplicatedQuestionContent =
            new Error("Question.DuplicatedQuestionContent", "another question with same content already exists.");


    
}
}
