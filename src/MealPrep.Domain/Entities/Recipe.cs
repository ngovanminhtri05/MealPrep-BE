namespace MealPrep.Domain.Entities;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int PreparationTime { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
