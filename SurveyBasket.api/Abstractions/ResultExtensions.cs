﻿namespace SurveyBasket.api.Abstractions
{
    public static class ResultExtensions
    {
        public static ObjectResult ToProblem(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Can not convert success to a problem ");

            var problem = Results.Problem(statusCode: result.Error.StatusCode);
            var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
            problemDetails!.Extensions=new Dictionary<string, object?> 
            {
                { "error",  new[] { 
                    
                    result.Error.code,
                    result.Error.Description,
                
                  
                } }
            };


            return  new ObjectResult(problemDetails);
        }
    }
}
