using MyFood.Application.Entities;
using MyFood.Application;
using MyFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetSingle(string username);
        UserEntity GetByEmail(string email);
        void Add(UserEntity user);
        void Delete(string email);
        UserEntity Login(string email, string password);
        UserEntity Update(string email);
        IQueryable<UserEntity> GetAll(QueryParameters queryParameters);
        bool Save();
        int Count();
 
    }
}
