namespace MealPrep.Business.Models;

public class MealPlanRequest
{
    public int NumberOfDays { get; set; }
    public string DietaryRestrictions { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
    public int CalorieTarget { get; set; }
    public List<string> PreferredCategories { get; set; } = new List<string>();
}
