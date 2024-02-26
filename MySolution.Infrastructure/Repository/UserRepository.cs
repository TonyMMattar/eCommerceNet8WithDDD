using Microsoft.EntityFrameworkCore;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Repository;
using eCommerce.Infrastructure.Context;
using eCommerce.Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository
{
    public sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Constructors
        public UserRepository(MyDatabaseContext context) : base(context) { }
        #endregion

        #region Methods
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await base.GetList().OrderBy(c => c.Email).ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await base.GetAsync(p => p.Id.Equals(id));
        }

        public async Task<User> GetCredentialsAsync(User user)
        {
            return await base.GetAsync(p => p.Email == user.Email && p.Password == user.Password);
        }
        #endregion
    }
}