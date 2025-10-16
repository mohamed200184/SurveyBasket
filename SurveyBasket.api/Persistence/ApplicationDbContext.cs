using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.api.Persistence.EntitiesConfigurations;
using System.Reflection;
using System.Security.Claims;

namespace SurveyBasket.api.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IHttpContextAccessor httpContextAccessor):IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Poll> polls { get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteAnswer>VoteAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var cascadeFks = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
          
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AutitableEntity>();
            foreach (var entityEntry in entries)
            {
                var currendUserId = httpContextAccessor.HttpContext?.User.GetUserId();
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x=>x.CreatedById).CurrentValue = currendUserId!;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x => x.UpdatedById).CurrentValue = currendUserId;

                    entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
