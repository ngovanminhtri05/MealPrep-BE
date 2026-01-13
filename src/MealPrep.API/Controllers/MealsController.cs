using Microsoft.AspNetCore.Mvc;
using MealPrep.Business.Interfaces;
using MealPrep.Domain.Entities;

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
    public async Task<ActionResult<Meal>> Create(Meal meal)
    {
        var createdMeal = await _mealService.CreateMealAsync(meal);
        return CreatedAtAction(nameof(GetById), new { id = createdMeal.Id }, createdMeal);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Meal>> Update(int id, Meal meal)
    {
        if (id != meal.Id)
            return BadRequest();

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
