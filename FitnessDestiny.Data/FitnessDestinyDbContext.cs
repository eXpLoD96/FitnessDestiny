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

        public DbSet<Supplement> Supplements { get; set; }

        public DbSet<ArticleComment> Comments { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<TraineeProgram> TraineePrograms {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .Entity<TraineeProgram>()
                .HasKey(st => new { st.ProgramId, st.TraineeId });

            builder
                .Entity<TraineeProgram>()
                .HasOne(tp => tp.Trainee)
                .WithMany(p => p.ProgramsEnrolled)
                .HasForeignKey(tp => tp.TraineeId);

            builder
                .Entity<TraineeProgram>()
                .HasOne(tp => tp.Program)
                .WithMany(t => t.Clients)
                .HasForeignKey(tp => tp.ProgramId);

            builder
                .Entity<Program>()
                .HasOne(p => p.Trainer)
                .WithMany(p => p.ProgramsTrained)
                .HasForeignKey(p => p.TrainerId);

            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId);

            builder.Entity<User>()
                .HasMany(u => u.ArticleComments)
                .WithOne(fc => fc.Author)
                .HasForeignKey(fc => fc.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
