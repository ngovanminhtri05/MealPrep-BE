using Microsoft.AspNetCore.Mvc;
using MealPrep.Business.Interfaces;
using MealPrep.Domain.Entities;

namespace MealPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
    {
        var recipes = await _recipeService.GetAllRecipesAsync();
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetById(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null)
            return NotFound();
        return Ok(recipe);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetByCategory(string category)
    {
        var recipes = await _recipeService.GetRecipesByCategoryAsync(category);
        return Ok(recipes);
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> Create(Recipe recipe)
    {
        var createdRecipe = await _recipeService.CreateRecipeAsync(recipe);
        return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Recipe>> Update(int id, Recipe recipe)
    {
        if (id != recipe.Id)
            return BadRequest();

        var updatedRecipe = await _recipeService.UpdateRecipeAsync(recipe);
        return Ok(updatedRecipe);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _recipeService.DeleteRecipeAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
