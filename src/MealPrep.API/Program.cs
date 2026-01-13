using Microsoft.EntityFrameworkCore;
using MealPrep.Data;
using MealPrep.Data.Repositories;
using MealPrep.Business.Interfaces;
using MealPrep.Business.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with InMemory database
builder.Services.AddDbContext<MealPrepDbContext>(options =>
    options.UseInMemoryDatabase("MealPrepDb"));

// Register repositories
builder.Services.AddScoped<RecipeRepository>();
builder.Services.AddScoped<MealRepository>();

// Register services
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IAIService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiKey = configuration["OpenAI:ApiKey"] ?? "demo-key";
    var model = configuration["OpenAI:Model"] ?? "gpt-4";
    return new AIService(apiKey, model);
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Seed sample data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MealPrepDbContext>();
    SeedData(context);
}

app.Run();

void SeedData(MealPrepDbContext context)
{
    if (!context.Recipes.Any())
    {
        var recipe1 = new MealPrep.Domain.Entities.Recipe
        {
            Name = "Grilled Chicken Salad",
            Description = "A healthy and delicious grilled chicken salad",
            Instructions = "1. Grill chicken breast\n2. Chop vegetables\n3. Mix all ingredients\n4. Add dressing",
            PreparationTime = 15,
            CookingTime = 20,
            Servings = 2,
            Category = "Salad",
            Difficulty = "Easy",
            Ingredients = new List<MealPrep.Domain.Entities.Ingredient>
            {
                new() { Name = "Chicken Breast", Quantity = 2, Unit = "pieces" },
                new() { Name = "Lettuce", Quantity = 200, Unit = "grams" },
                new() { Name = "Tomatoes", Quantity = 3, Unit = "pieces" },
                new() { Name = "Olive Oil", Quantity = 2, Unit = "tablespoons" }
            }
        };

        var recipe2 = new MealPrep.Domain.Entities.Recipe
        {
            Name = "Spaghetti Carbonara",
            Description = "Classic Italian pasta dish",
            Instructions = "1. Cook pasta\n2. Fry bacon\n3. Mix eggs and cheese\n4. Combine all ingredients",
            PreparationTime = 10,
            CookingTime = 25,
            Servings = 4,
            Category = "Pasta",
            Difficulty = "Medium",
            Ingredients = new List<MealPrep.Domain.Entities.Ingredient>
            {
                new() { Name = "Spaghetti", Quantity = 400, Unit = "grams" },
                new() { Name = "Bacon", Quantity = 200, Unit = "grams" },
                new() { Name = "Eggs", Quantity = 4, Unit = "pieces" },
                new() { Name = "Parmesan", Quantity = 100, Unit = "grams" }
            }
        };

        context.Recipes.AddRange(recipe1, recipe2);
        context.SaveChanges();
    }
}
