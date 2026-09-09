using Microsoft.EntityFrameworkCore;

namespace ToDo.API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         modelBuilder.HasPostgresExtension("uuid-ossp");

        // Foreign Key Relationships Configuration
        modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.Category)
            .WithMany(c => c.TodoItems)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Id)
              .HasDefaultValueSql("uuid_generate_v4()");
            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        var adminUser = new User { Id = Guid.Parse("9b016f5d-3c2f-4e12-bf9e-bf3cf88ef492"), Username = "bita" };

        // Keep this value static. Using PasswordHasher here would generate a different hash every build,
        // which causes EF Core to think the model changed each time and triggers the pending model warning.
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUser.Id,
                Username = adminUser.Username,
                Email = "bita@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEL1PmuH77B6HFaZ/zje5tI3K+Irso3y6o78VHZ88ZkqRj8geWEgjp8Cl2tFxRFH45Q==",
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Work",
                ColorHex = "#0d6efd",
                UserId = adminUser.Id
            },
            new Category
            {
                Id = 2,
                Name = "Personal",
                ColorHex = "#198754",
                UserId = adminUser.Id
            }
        );

        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem
            {
                Id = 1,
                Title = "Set up Angular Frontend",
                Description = "Initialize Angular standalone app and create navigation",
                IsCompleted = false,
                Priority = Priority.High,
                DueDate = new DateTime(2026, 10, 10, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                UserId = adminUser.Id,
                CategoryId = 1
            },
            new TodoItem
            {
                Id = 2,
                Title = "Buy groceries",
                Description = "Milk, eggs, and bread",
                IsCompleted = true,
                Priority = Priority.Low,
                DueDate = null,
                CreatedDate = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                UserId = adminUser.Id,
                CategoryId = 2
            }
        );
    }
}