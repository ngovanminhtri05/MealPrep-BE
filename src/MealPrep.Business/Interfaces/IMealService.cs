using MealPrep.Domain.Entities;

namespace MealPrep.Business.Interfaces;

public interface IMealService
{
    Task<Meal?> GetMealByIdAsync(int id);
    Task<IEnumerable<Meal>> GetAllMealsAsync();
    Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Meal> CreateMealAsync(Meal meal);
    Task<Meal> UpdateMealAsync(Meal meal);
    Task<bool> DeleteMealAsync(int id);
}
