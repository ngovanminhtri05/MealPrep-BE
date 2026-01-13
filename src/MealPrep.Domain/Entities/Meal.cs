namespace MealPrep.Domain.Entities;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string MealType { get; set; } = string.Empty;
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int? UserPreferenceId { get; set; }
    public UserPreference? UserPreference { get; set; }
}
