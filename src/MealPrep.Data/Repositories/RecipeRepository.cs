using MealPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPrep.Data.Repositories;

public class RecipeRepository : Repository<Recipe>
{
    public RecipeRepository(MealPrepDbContext context) : base(context)
    {
    }

    public override async Task<Recipe?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public override async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await _dbSet
            .Include(r => r.Ingredients)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> GetByCategoryAsync(string category)
    {
        return await _dbSet
            .Include(r => r.Ingredients)
            .Where(r => r.Category == category)
            .ToListAsync();
    }
}
