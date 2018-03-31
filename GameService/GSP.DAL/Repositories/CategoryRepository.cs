using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.Games.DAL.Context;
using GSP.Games.DAL.Repositories.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.Games.DAL.Repositories
{
    public class CategoryRepository : GameStoreRepository<Category, GameStoreGameContext>, ICategoryRepository
    {
        private readonly GameStoreGameContext _dbContext;

        public CategoryRepository(GameStoreGameContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories.AsEnumerable();
        }

        public Category GetCategoryByName(string name)
        {
            return _dbContext.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
