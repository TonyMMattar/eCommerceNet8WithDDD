using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eCommerce.Domain.Entities;

namespace eCommerce.Infrastructure.FluentApi
{
    public class ProductFluent : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);
        }
    }
}