using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.api.Persistence.EntitiesConfigurations
{
    public class AnswerConfigration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(1000);
            builder.HasIndex(x => new { x.QuestionId,x.Content}).IsUnique();
        }
    }
}
