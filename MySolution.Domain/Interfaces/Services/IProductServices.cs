using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetListAsync(int skip, int take);
        Task<int> GetListCount();
        Task<Product> GetAsync(int id);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}