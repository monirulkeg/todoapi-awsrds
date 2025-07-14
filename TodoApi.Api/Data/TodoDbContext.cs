using Microsoft.EntityFrameworkCore;
using TodoApi.Api.Models;

namespace TodoApi.Api.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Todo entity
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Priority).HasMaxLength(50).HasDefaultValue("Medium");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            // Create index for performance
            entity.HasIndex(e => e.IsCompleted);
            entity.HasIndex(e => e.CreatedAt);
            entity.HasIndex(e => e.Priority);
        });

        // Seed data with static datetime values
        var seedDate = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);
        
        modelBuilder.Entity<Todo>().HasData(
            new Todo
            {
                Id = 1,
                Title = "Complete project setup",
                Description = "Set up the initial .NET API project with Entity Framework and PostgreSQL",
                Priority = "High",
                Category = "Development",
                CreatedAt = seedDate,
                IsCompleted = false
            },
            new Todo
            {
                Id = 2,
                Title = "Implement CRUD operations",
                Description = "Create endpoints for Create, Read, Update, and Delete operations for todos",
                Priority = "High",
                Category = "Development",
                CreatedAt = seedDate.AddMinutes(5),
                IsCompleted = false
            },
            new Todo
            {
                Id = 3,
                Title = "Set up CI/CD pipeline",
                Description = "Configure GitHub Actions for continuous integration and deployment",
                Priority = "Medium",
                Category = "DevOps",
                CreatedAt = seedDate.AddMinutes(10),
                IsCompleted = false
            }
        );
    }
}
