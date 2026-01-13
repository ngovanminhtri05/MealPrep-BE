using MealPrep.Business.Interfaces;
using MealPrep.Data.Repositories;
using MealPrep.Domain.Entities;

namespace MealPrep.Business.Services;

public class RecipeService : IRecipeService
{
    private readonly RecipeRepository _recipeRepository;

    public RecipeService(RecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _recipeRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
    {
        return await _recipeRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Recipe>> GetRecipesByCategoryAsync(string category)
    {
        return await _recipeRepository.GetByCategoryAsync(category);
    }

    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        return await _recipeRepository.AddAsync(recipe);
    }

    public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
    {
        return await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        return await _recipeRepository.DeleteAsync(id);
    }
}
