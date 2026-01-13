using MealPrep.Business.Interfaces;
using MealPrep.Data.Repositories;
using MealPrep.Domain.Entities;

namespace MealPrep.Business.Services;

public class MealService : IMealService
{
    private readonly MealRepository _mealRepository;

    public MealService(MealRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }

    public async Task<Meal?> GetMealByIdAsync(int id)
    {
        return await _mealRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Meal>> GetAllMealsAsync()
    {
        return await _mealRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _mealRepository.GetByDateRangeAsync(startDate, endDate);
    }

    public async Task<Meal> CreateMealAsync(Meal meal)
    {
        return await _mealRepository.AddAsync(meal);
    }

    public async Task<Meal> UpdateMealAsync(Meal meal)
    {
        return await _mealRepository.UpdateAsync(meal);
    }

    public async Task<bool> DeleteMealAsync(int id)
    {
        return await _mealRepository.DeleteAsync(id);
    }
}
