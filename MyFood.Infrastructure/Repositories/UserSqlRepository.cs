using MyFood.Application;
using MyFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood.Infrastructure.Repositories
{
    public class UserSqlRepository : IUserRepository
    {
        private readonly FoodDbContext _FoodDbContext;
        private readonly IPasswordHasher _passwordHasher;

        public UserSqlRepository(FoodDbContext foodDbContext, IPasswordHasher passwordHasher)
        {
            _FoodDbContext = foodDbContext;
            _passwordHasher = passwordHasher;
        }

        public public RecipeEntity GetSingle(int id)
        {
            return _foodDbContext.Recipes.FirstOrDefault(x => x.RecipeId == id);
        }
        public void Add(UserEntity user)
        {
            _FoodDbContext.Users.Add(user);
        }

        public int Count()
        {
            return _FoodDbContext.Users.Count();
        }

        void IUserRepository.Delete(string email)
        {
            UserEntity user = GetSingle(email);
            _FoodDbContext.Users.Remove(user);
        }

        public IQueryable<UserEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<UserEntity> query = _FoodDbContext.Users;
            return query.AsQueryable();
        }

        public UserEntity GetSingle(string name)
        {
            return _FoodDbContext.Users.FirstOrDefault(x => x.Name == name);
        }

        public UserEntity GetByEmail(string email)
        {
            return _FoodDbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public UserEntity Login(string name, string password)
        {
            UserEntity user = _FoodDbContext.Users.FirstOrDefault(u => u.Email == name);
            if (user == null || !_passwordHasher.Verify(user.Password, password ))
            {
                return null;
            }
            return user;
        }

        public bool Save()
        {
            return (_FoodDbContext.SaveChanges() >= 0);
        }

        public UserEntity Update(string email)
        {
            var user = _FoodDbContext.Users.FirstOrDefault(u => u.Email == email);
            _FoodDbContext.Users.Update(user);
            return user;
        }
    }
}
