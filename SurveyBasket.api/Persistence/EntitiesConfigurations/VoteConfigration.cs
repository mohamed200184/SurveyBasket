using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.api.Persistence.EntitiesConfigurations
{
    public class VoteConfigration:IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(x => new { x.PollId, x.UserId }).IsUnique();


        }

        
    }
}
