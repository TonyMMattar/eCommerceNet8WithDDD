/*
   This controller manages user-related operations.
   It provides endpoints for CRUD operations on user entities.

   Key Features:
   - Utilizes IUserServices interface for user management operations.
   - Requires authorization for accessing any action within this controller.
*/

using Microsoft.AspNetCore.Mvc;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Web.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Variables
        private readonly IUserServices _userServices;
        #endregion

        #region Constructors
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Actions
        [HttpGet("[action]")]
        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userServices.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetAsync(int id)
        {
            return await _userServices.GetAsync(id);
        }

        [HttpPost]
        public async Task<bool> AddAsync([FromBody] User user)
        {
            return await _userServices.AddAsync(user);
        }

        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            return await _userServices.UpdateAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _userServices.DeleteAsync(id);
        }
        #endregion
    }
}