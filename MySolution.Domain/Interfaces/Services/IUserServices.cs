using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetListAsync();
        Task<User> GetAsync(int id);
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User> GetCredentialsAsync(User user);
    }
}