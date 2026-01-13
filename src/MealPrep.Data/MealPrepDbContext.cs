using Microsoft.EntityFrameworkCore;
using MealPrep.Domain.Entities;

namespace MealPrep.Data;

public class MealPrepDbContext : DbContext
{
    public MealPrepDbContext(DbContextOptions<MealPrepDbContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<UserPreference> UserPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Instructions).HasMaxLength(5000);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Difficulty).HasMaxLength(20);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasPrecision(10, 2);

            entity.HasOne(e => e.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(e => e.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.MealType).HasMaxLength(50);

            entity.HasOne(e => e.Recipe)
                .WithMany(r => r.Meals)
                .HasForeignKey(e => e.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UserPreference)
                .WithMany(u => u.Meals)
                .HasForeignKey(e => e.UserPreferenceId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DietaryRestrictions).HasMaxLength(500);
            entity.Property(e => e.Allergies).HasMaxLength(500);
            entity.Property(e => e.FavoriteCategories).HasMaxLength(500);
        });
    }
}
