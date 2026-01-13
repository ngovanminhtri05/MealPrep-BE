using MealPrep.Domain.Entities;

namespace MealPrep.Business.Interfaces;

public interface IRecipeService
{
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<IEnumerable<Recipe>> GetRecipesByCategoryAsync(string category);
    Task<Recipe> CreateRecipeAsync(Recipe recipe);
    Task<Recipe> UpdateRecipeAsync(Recipe recipe);
    Task<bool> DeleteRecipeAsync(int id);
}
