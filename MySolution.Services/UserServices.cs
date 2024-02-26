/*
   This service class implements business logic related to users.
   It utilizes a repository (IUserRepository) for data access operations.

   Key Features:
   - Methods for retrieving a list of users, getting a specific user by ID, and authenticating user credentials.
   - CRUD operations for adding, updating, and deleting users.
   - Validation of business rules before saving or updating users.

   Business Rules Validation:
   - Ensures the user ID is valid for updates.
   - Ensures the user email is not empty or null.

   Note:
   - Business rule validation with the ValidateToSave method.
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
    public sealed class UserServices : IUserServices
    {
        #region Variables
        private readonly IUserRepository _repository;
        #endregion

        #region Constructors
        public UserServices(IUserRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            if (id > 0)
                return await _repository.GetAsync(id);
            return null;
        }

        public async Task<bool> AddAsync(User user)
        {
            await _repository.AddAsync(user);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            ValidateToSave(user, true);

            _repository.Update(user);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _repository.Delete(await GetAsync(id));
            return await _repository.SaveChangesAsync();
        }

        public async Task<User> GetCredentialsAsync(User user)
        {
            return await _repository.GetCredentialsAsync(user);
        }

        /// <summary>
        /// Example for validation of business rules.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="update"></param>
        private void ValidateToSave(User user, bool update)
        {
            if (update)
            {
                if (user.Id == Guid.Empty)
                    throw new ApplicationException($"Invalid {nameof(user.Id)} to update the {nameof(user)}.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ApplicationException($"Empty ({nameof(user.Email)}) for the {nameof(user)}.");
        }

        
        #endregion
    }
}