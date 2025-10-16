using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.api.Persistence.EntitiesConfigurations
{
    public class VoteAnswerConfigration : IEntityTypeConfiguration<VoteAnswer>
    {
        public void Configure(EntityTypeBuilder<VoteAnswer> builder)
        {
            builder.HasIndex(x => new { x.VoteId, x.QuestionId }).IsUnique();
        }
    }
}
