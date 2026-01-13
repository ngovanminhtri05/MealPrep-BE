using MealPrep.Business.Models;

namespace MealPrep.Business.Interfaces;

public interface IAIService
{
    Task<string> GenerateMealPlanAsync(MealPlanRequest request);
    Task<string> GetRecipeSuggestionsAsync(string preferences);
    Task<string> AnalyzeNutritionalInfoAsync(string recipeName, List<string> ingredients);
}
