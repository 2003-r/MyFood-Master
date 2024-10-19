using MyFood.Domain.Entities;
using MyFood.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood.Infrastructure.Repositories
{
    public class RecipeSqlRepository : IRecipeRepository
    {

        private readonly FoodDbContext _foodDbContext;

        public RecipeSqlRepository(FoodDbContext foodDbContext)
        {
            _foodDbContext = foodDbContext;
        }

        public RecipeEntity GetSingle(int id)
        {
            return _foodDbContext.Recipes.FirstOrDefault(x => x.RecipeId == id);
        }

        public void Add(RecipeEntity item)
        {
            _foodDbContext.Recipes.Add(item);
        }

        public void Delete(int id)
        {
            RecipeEntity Recipe = GetSingle(id);
            _foodDbContext.Recipes.Remove(Recipe);
        }

        public RecipeEntity Update(int id, RecipeEntity item)
        {
            _foodDbContext.Recipes.Update(item);
            return item;
        }

        public IQueryable<RecipeEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<RecipeEntity> _allItems = _foodDbContext.Recipes.OrderBy(x => x.Title);

            
            
            _allItems = _allItems
                    .Where(x =>  x.Title.ToLowerInvariant()
                    .Contains(queryParameters.Query.ToLowerInvariant()));
            

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _foodDbContext.Recipes.Count();
        }

        public bool Save()
        {
            return (_foodDbContext.SaveChanges() >= 0);
        }

        public ICollection<RecipeEntity> GetRandomMeal()
        {
            List<RecipeEntity> toReturn = new List<RecipeEntity>();

            toReturn.Add(GetRandomItem("Starter"));
            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        private RecipeEntity GetRandomItem(string category)
        {
            return _foodDbContext.Recipes
                .Where(x => x.Category == category)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
