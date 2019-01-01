using GSP.DAL.EF.Context;
using GSP.DAL.EF.Repositories;
using GSP.Game.DAL.Repositories.Contracts;
using GSP.Game.Domain.Games;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.DAL.EF.Repositories
{
    public class CategoryRepository : BaseRepository<int, Category, GameContext>, ICategoryRepository
    {
        private readonly GameContext _dbContext;

        public CategoryRepository(GameContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken ct = default)
        {
            return await _dbContext.Categories.ToListAsync(ct);
        }

        public async Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct = default)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), ct);
        }
    }
}