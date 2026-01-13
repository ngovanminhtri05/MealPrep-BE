using Microsoft.AspNetCore.Mvc;
using MealPrep.Business.Interfaces;
using MealPrep.Business.Models;

namespace MealPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;

    public AIController(IAIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("meal-plan")]
    public async Task<ActionResult<string>> GenerateMealPlan([FromBody] MealPlanRequest request)
    {
        try
        {
            var mealPlan = await _aiService.GenerateMealPlanAsync(request);
            return Ok(new { mealPlan });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("recipe-suggestions")]
    public async Task<ActionResult<string>> GetRecipeSuggestions([FromBody] string preferences)
    {
        try
        {
            var suggestions = await _aiService.GetRecipeSuggestionsAsync(preferences);
            return Ok(new { suggestions });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("nutritional-analysis")]
    public async Task<ActionResult<string>> AnalyzeNutritionalInfo([FromBody] NutritionalAnalysisRequest request)
    {
        try
        {
            var analysis = await _aiService.AnalyzeNutritionalInfoAsync(request.RecipeName, request.Ingredients);
            return Ok(new { analysis });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

public class NutritionalAnalysisRequest
{
    public string RecipeName { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new List<string>();
}
