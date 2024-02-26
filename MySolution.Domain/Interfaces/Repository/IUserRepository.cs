using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync();
        Task<User> GetAsync(int id);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<User> GetCredentialsAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}