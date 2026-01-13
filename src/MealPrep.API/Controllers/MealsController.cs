using Microsoft.AspNetCore.Mvc;
using MealPrep.Business.Interfaces;
using MealPrep.Domain.Entities;
using MealPrep.API.DTOs;

namespace MealPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly IMealService _mealService;

    public MealsController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetAll()
    {
        var meals = await _mealService.GetAllMealsAsync();
        return Ok(meals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Meal>> GetById(int id)
    {
        var meal = await _mealService.GetMealByIdAsync(id);
        if (meal == null)
            return NotFound();
        return Ok(meal);
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<Meal>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var meals = await _mealService.GetMealsByDateRangeAsync(startDate, endDate);
        return Ok(meals);
    }

    [HttpPost]
    public async Task<ActionResult<Meal>> Create(CreateMealRequest request)
    {
        var meal = new Meal
        {
            Name = request.Name,
            Date = request.Date,
            MealType = request.MealType,
            RecipeId = request.RecipeId,
            UserPreferenceId = request.UserPreferenceId
        };
        
        var createdMeal = await _mealService.CreateMealAsync(meal);
        return CreatedAtAction(nameof(GetById), new { id = createdMeal.Id }, createdMeal);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Meal>> Update(int id, CreateMealRequest request)
    {
        var meal = await _mealService.GetMealByIdAsync(id);
        if (meal == null)
            return NotFound();
        
        meal.Name = request.Name;
        meal.Date = request.Date;
        meal.MealType = request.MealType;
        meal.RecipeId = request.RecipeId;
        meal.UserPreferenceId = request.UserPreferenceId;

        var updatedMeal = await _mealService.UpdateMealAsync(meal);
        return Ok(updatedMeal);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _mealService.DeleteMealAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
