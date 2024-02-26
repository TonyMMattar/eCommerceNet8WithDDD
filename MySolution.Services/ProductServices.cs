/*
   This service class implements business logic related to products.
   It utilizes a repository (IProductRepository) for data access operations.

   Key Features:
   - Methods for retrieving a paginated list of products and getting the total count of products.
   - CRUD operations for adding, updating, and deleting products.
   - Validation of business rules before saving or updating products.

   Business Rules Validation:
   - Ensures the product ID is valid for updates.
   - Ensures the product name is not empty or null.

   Note:
   - Business rule validation is demonstrated with the ValidateToSave method.
   - The SaveChangesAsync method is provided by the repository for saving changes to the database.
*/

using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Repository;
using eCommerce.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public sealed class ProductServices : IProductServices
    {
        #region Variables
        private readonly IProductRepository _repository;
        #endregion

        #region Constructors
        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Product>> GetListAsync(int skip, int take)
        {
            return await _repository.GetListAsync(skip, take);
        }

        public async Task<int> GetListCount()
        {
            return await _repository.GetListCount();
        }

        public async Task<Product> GetAsync(int id)
        {
            if (id > 0)
                return await _repository.GetAsync(id);
            return null;
        }

        public async Task<bool> AddAsync(Product product)
        {
            ValidateToSave(product, false);

            await _repository.AddAsync(product);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            ValidateToSave(product, true);

            _repository.Update(product);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _repository.Delete(await GetAsync(id));
            return await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Example for validation of business rules.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="update"></param>
        private void ValidateToSave(Product product, bool update)
        {
            if (update)
            {
                if (product.Id < 1)
                    throw new ApplicationException($"Invalid Id to update {nameof(product)}.");
            }
            else
            {
                product.Id = 0;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ApplicationException($"Empty ({nameof(product.Name)}) for the {nameof(product)}.");
            
        }
        #endregion
    }
}