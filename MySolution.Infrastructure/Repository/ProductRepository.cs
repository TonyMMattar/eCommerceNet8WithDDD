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
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        #region Constructors
        public ProductRepository(MyDatabaseContext context) : base(context) { }
        #endregion

        #region Methods
        public async Task<IEnumerable<Product>> GetListAsync(int skip, int take)
        {
            return await base.GetList(skip, take)
                //.OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<int> GetListCount()
        {
            return base.GetListCount();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await base.GetList()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        #endregion
    }
}