using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetListAsync(int skip, int take);
        Task<int> GetListCount();
        Task<Product> GetAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task<bool> SaveChangesAsync();
    }
}