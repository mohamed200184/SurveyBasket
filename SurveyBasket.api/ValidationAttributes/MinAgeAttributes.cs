using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.api.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class MinAgeAttributes(int minAge) : ValidationAttribute
    {

        private readonly int _minAge;

        //public override bool IsValid(object? value)
        //{
        //    if(value is not null)
        //    {
        //        var date = (DateTime)value;
        //        if (DateTime.Today < date.AddYears(_minAge)) 
        //            return false;
        //    }

        //    return true;
        //}


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
         {
             var date = (DateTime)value;
               if (DateTime.Today < date.AddYears(_minAge)) 
                  return new ValidationResult($" invalide {validationContext.DisplayName}, age  is {18} yeasrs old");
         }

                   return ValidationResult.Success;
        }
    }
}
