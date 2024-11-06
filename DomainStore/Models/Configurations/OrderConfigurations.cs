using DomainStore.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Models.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(x => x.DeliveryMethodId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Items)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.Status)
                   .HasConversion(x => x.ToString(), x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x));

            builder.Property(x => x.SubTotal)
                   .HasColumnType("decimal(18,2)");

            builder.OwnsOne(x => x.ShippingAddress, x => x.WithOwner());
        }
    }
}
