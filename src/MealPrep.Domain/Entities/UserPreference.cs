namespace MealPrep.Domain.Entities;

public class UserPreference
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string DietaryRestrictions { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
    public string FavoriteCategories { get; set; } = string.Empty;
    public int CalorieTarget { get; set; }
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
