using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.Domain.Entities;

namespace eCommerce.Infrastructure.Context
{
    public class MyDatabaseContext : IdentityDbContext
    {
        #region Constructors
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options) { }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FluentApi.UserFluent());
            modelBuilder.ApplyConfiguration(new FluentApi.ProductFluent());
        }
        #endregion
    }
}