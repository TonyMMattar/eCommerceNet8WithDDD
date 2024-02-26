using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eCommerce.Domain.Entities;

namespace eCommerce.Infrastructure.FluentApi
{
    public class UserFluent : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
        }
    }
}