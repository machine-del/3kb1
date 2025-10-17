using TestingPlatform.Domain.Models;
using TestingPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestingPlatform.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Direction> Directions => Set<Direction>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();
        public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();
        public DbSet<UserAttemptsAnswer> UserAttemptAnswers => Set<UserAttemptsAnswer>();
        public DbSet<TestResult> TestResults => Set<TestResult>();
        public DbSet<Test> Tests => Set<Test>();
        public DbSet<Questions> Questions => Set<Questions>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Attempt> Attempts => Set<Attempt>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Login).IsUnique();
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.Login).IsRequired();
                e.Property(x => x.Email).IsRequired();
                e.Property(x => x.FirtsName).IsRequired().HasMaxLength(50);
                e.Property(x => x.LastName).IsRequired().HasMaxLength(50);
                e.Property(x => x.MiddleName).IsRequired(false);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


                e.HasOne(x => x.Student)
                    .WithOne(s => s.User)
                    .HasForeignKey<Student>(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Phone).HasMaxLength(30).IsRequired(false);
                e.Property(x => x.VKLink).IsRequired(false);
                e.Property(x => x.UserId).IsRequired();
            });

            modelBuilder.Entity<Direction>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Group>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();

                e.HasOne(x => x.Direction)
                    .WithMany(d => d.Groups)
                    .HasForeignKey(x => x.DirectionId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Course)
                    .WithMany(c => c.Groups)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Project)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Test>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).IsRequired().HasMaxLength(1000);
                e.Property(x => x.IsRepeatable).HasDefaultValue(false);
                e.Property(x => x.Type).HasConversion<string>().HasMaxLength(20);
                e.Property(x => x.Type).HasConversion<string>().HasMaxLength(20);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(x => x.PublishedAt).IsRequired();
                e.Property(x => x.Deadline).IsRequired();
                e.Property(x => x.DurationMinutes).IsRequired(false);
                e.Property(x => x.IsPublic).HasDefaultValue(false);
                e.Property(x => x.PassingScore).IsRequired(false);
                e.Property(x => x.MaxAttempts).IsRequired(false);

                //e.HasMany(x => x.Groups)
                //   .WithMany(g => g.Tests)
                //   .UsingEntity(j => j.ToTable("test_groups"));

                //e.HasMany(x => x.Projects)
                //    .WithMany(p => p.Tests)
                //    .UsingEntity(j => j.ToTable("test_groups"));

                //e.HasMany(x => x.Courses)
                //    .WithMany(c => c.Tests)
                //    .UsingEntity(j => j.ToTable("test_groups"));

                //e.HasMany(x => x.Directions)
                //    .WithMany(d => d.Tests)
                //    .UsingEntity(j => j.ToTable("test_groups"));
            });

            modelBuilder.Entity<Questions>(e =>
            {
                e.HasKey(q => q.Id);
                e.Property(q => q.Text).IsRequired().HasMaxLength(4000);
                e.Property(q => q.Number).IsRequired();
                e.Property(q => q.Description).HasMaxLength(2000);
                e.Property(q => q.AnswerType).HasConversion<string>().HasMaxLength(50);
                e.Property(q => q.IsScoring).HasDefaultValue(true);
                e.Property(q => q.Maxscore).IsRequired(false);
                e.Property(q => q.TestId).IsRequired();

                e.HasOne(q => q.Test)
                    .WithMany(t => t.Questions)
                    .HasForeignKey(q => q.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => new { x.TestId, x.Number })
                    .IsUnique();
            });

            modelBuilder.Entity<Answer>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Text).IsRequired().HasMaxLength(1000);
                e.Property(a => a.IsCorrect).IsRequired();
                e.Property(a => a.QuestionId).IsRequired();

                e.HasOne(a => a.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Attempt>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(a => a.Score).IsRequired(false);
                e.Property(a => a.TestId).IsRequired();
                e.Property(a => a.StudentId).IsRequired();

                e.HasOne(a => a.Test)
                    .WithMany()
                    .HasForeignKey(a => a.TestId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(a => a.Student)
                    .WithMany()
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserAttemptsAnswer>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.IsCorrect).IsRequired();
                e.Property(x => x.ScoreAwarded).IsRequired();
                e.Property(x => x.AttemptId).IsRequired();
                e.Property(x => x.QuestionId).IsRequired();

                e.HasIndex(x => new { x.AttemptId, x.QuestionId })
                    .IsUnique();
            });

            modelBuilder.Entity<UserSelectedOption>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.UserAttemptAnswerId).IsRequired();
                e.Property(x => x.AnswerId).IsRequired();

                e.HasOne(x => x.UserAttemptAnswer)
                    .WithMany(x => x.UserSelectedOptions)
                    .HasForeignKey(x => x.UserAttemptAnswerId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Answer)
                    .WithMany()
                    .HasForeignKey(x => x.AnswerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserTextAnswer>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.TextAnswer).IsRequired().HasMaxLength(4000);
                e.Property(x => x.UserAttemptAnswerId).IsRequired();

                e.HasOne(x => x.UserAttemptsAnswer)
                    .WithOne(x => x.UserTextAnswer)
                    .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TestResult>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Passed).IsRequired();
                e.Property(x => x.TestId).IsRequired();
                e.Property(x => x.AttemptId).IsRequired();
                e.Property(x => x.StudentId).IsRequired();

                e.HasOne(x => x.Test)
                    .WithMany()
                    .HasForeignKey(x => x.TestId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Attempt)
                    .WithMany()
                    .HasForeignKey(x => x.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Student)
                    .WithMany()
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId })
                    .IsUnique();
            });
        }
    }
}