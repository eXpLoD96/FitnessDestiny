namespace FitnessDestiny.Data
{
    using FitnessDestiny.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FitnessDestinyDbContext : IdentityDbContext<User>
    {
        public FitnessDestinyDbContext(DbContextOptions<FitnessDestinyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId);
        }
    }
}
