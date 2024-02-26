using System;

namespace eCommerce.Domain.Entities
{
    public class User
    {
        #region Properties
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        #endregion
    }
}