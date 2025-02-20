using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microstore.Service.OrderingDomain.Enums;
using Microstore.Service.OrderingDomain.Models;
using Microstore.Service.OrderingDomain.ValueObjects;

namespace Microstore.Service.OrderingInfrastructure.Data.Configurations;

public class OrderConfiguration 
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion
            (
                OrderId => OrderId.Value,
                dbId => OrderId.Of(dbId)
            );

        builder.ComplexProperty
        (
            c => c.OrderName,
            nameBuilder =>
            {
                nameBuilder
                    .Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            }
        );

        builder
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .IsRequired();

        builder
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty
        (
            o => o.ShippingAddress, 
            addressBuilder =>
            {
                addressBuilder
                    .Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                addressBuilder
                    .Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.Country)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.State)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();

            }
        );

        builder.ComplexProperty
        (
            o => o.BillingAddress,
            addressBuilder =>
            {
                addressBuilder
                    .Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                addressBuilder
                    .Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.Country)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.State)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();

            }
        );

        builder.ComplexProperty
        (
            o => o.Payment, 
            paymentBuilder =>
            {
                paymentBuilder
                    .Property(p => p.CardName)
                    .HasMaxLength(50)
                    .IsRequired();
                paymentBuilder
                    .Property(p => p.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();
                paymentBuilder
                    .Property(p => p.Expiration)
                    .HasMaxLength(10);
                paymentBuilder
                    .Property(p => p.CVV)
                    .HasMaxLength(3)
                    .IsRequired();
                paymentBuilder
                    .Property(p => p.PaymentMethod);
            }
        );

        builder
            .Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion
            (
                orderStatus => orderStatus.ToString(),
                dbValue => Enum.Parse<OrderStatus>(dbValue)
            );

        builder.Property(o => o.TotalPrice);
    }
}