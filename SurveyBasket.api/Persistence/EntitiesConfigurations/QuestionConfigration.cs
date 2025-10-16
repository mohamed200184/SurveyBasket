using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.api.Persistence.EntitiesConfigurations
{
    public class QuestionConfigration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(1000);
            builder.HasIndex(x => new {x.PollId ,x.Content } ).IsUnique();
        }
    }
}
