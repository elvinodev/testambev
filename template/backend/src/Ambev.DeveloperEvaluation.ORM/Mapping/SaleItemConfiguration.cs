using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

[ExcludeFromCodeCoverage]
public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        // Chave primária
        builder.HasKey(si => si.Id);

        builder.Property(si => si.Product)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(si => si.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        //builder.Property(si => si.TotalItemAmount)
        //    .HasColumnType("decimal(18,2)");
            //.HasComputedColumnSql("[UnitPrice] * [Quantity] - [Discount]", stored: true);
    }
}

