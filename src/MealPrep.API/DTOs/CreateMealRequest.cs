namespace MealPrep.API.DTOs;

public class CreateMealRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string MealType { get; set; } = string.Empty;
    public int RecipeId { get; set; }
    public int? UserPreferenceId { get; set; }
}
