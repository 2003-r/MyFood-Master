using MyFood.Application.Entities;
using MyFood.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFood.Domain.Entities;

namespace MyFood.Infrastructure.Repositories
{
    public interface IRecipeRepository
    {
        RecipeEntity GetSingle(int id);
        void Add(RecipeEntity item);
        void Delete(int id);
        RecipeEntity Update(int id, RecipeEntity item);
        IQueryable<RecipeEntity> GetAll(QueryParameters queryParameters);
        ICollection<RecipeEntity> GetRandomMeal();
        int Count();
        bool Save();
    }
}
