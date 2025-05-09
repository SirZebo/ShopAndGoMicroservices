﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId)
        );
        builder.Property(o => o.TransactionToken);
        builder.Property(o => o.MerchantId);

        builder.Property(o => o.MaxCompletionTime).HasConversion(
            maxCompletionTime => maxCompletionTime.TotalDays,
            maxCompletionTimeInDays => TimeSpan.FromDays(maxCompletionTimeInDays)
        );

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
             o => o.ShippingAddress, addressBuilder =>
             {
                 addressBuilder.Property(a => a.FirstName)
                     .HasMaxLength(50)
                     .IsRequired();

                 addressBuilder.Property(a => a.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                 addressBuilder.Property(a => a.EmailAddress)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                 addressBuilder.Property(a => a.Country)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.State)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
             });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.AwaitingPayment)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), dbStatus));
    }
}
