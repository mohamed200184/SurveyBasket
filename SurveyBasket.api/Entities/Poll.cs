

namespace SurveyBasket.api.Entities
{
    public sealed class Poll :AutitableEntity
    {

       
        public int Id { get; set; }

        
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;
      
        public bool IsPublished { get; set; }

        public DateOnly startsAt { get; set; }

        public DateOnly EndAt { get; set; }
        
    }
}
