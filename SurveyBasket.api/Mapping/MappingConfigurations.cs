﻿using SurveyBasket.api.Contracts.Questions;

namespace SurveyBasket.api.Mapping
{
    public  class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<QuestionRequest, Question>()
                .Map(dest => dest.Answers, src => src.Answers.Select(answer=>new Answer { Content=answer}));
                
        }
    }
}
