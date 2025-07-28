namespace SurveyBasket.api.Entities
{
    public class AutitableEntity
    {

        //logges
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        //updated loggs
        public string? UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;

        public ApplicationUser CreatedBy { get; set; } = default!;
        public ApplicationUser? UpdatedBy { get; set; }

    }
}
