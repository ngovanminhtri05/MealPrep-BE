using MealPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPrep.Data.Repositories;

public class MealRepository : Repository<Meal>
{
    public MealRepository(MealPrepDbContext context) : base(context)
    {
    }

    public override async Task<Meal?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(m => m.Recipe)
                .ThenInclude(r => r.Ingredients)
            .Include(m => m.UserPreference)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public override async Task<IEnumerable<Meal>> GetAllAsync()
    {
        return await _dbSet
            .Include(m => m.Recipe)
                .ThenInclude(r => r.Ingredients)
            .Include(m => m.UserPreference)
            .ToListAsync();
    }

    public async Task<IEnumerable<Meal>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(m => m.Recipe)
                .ThenInclude(r => r.Ingredients)
            .Include(m => m.UserPreference)
            .Where(m => m.Date >= startDate && m.Date <= endDate)
            .OrderBy(m => m.Date)
            .ToListAsync();
    }
}
