using Dsw2025Tpi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.ShippingAddress).IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.BillingAddress).IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.Notes)
                .IsRequired(false);

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Ignore(o => o.TotalAmount);

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.CustomerId).IsRequired();


        }
    }
}
