using EF_Core_S04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Configurations
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Primary Key
            builder.HasKey(T => T.TransactionNumber);

            // Properties (Columns)
            builder.Property(T => T.TransactionDate);
            builder.Property(T => T.Amount).HasPrecision(18, 2);
            builder.Property(T => T.TransactionType);
            builder.Property(T => T.Note).HasMaxLength(250);

            // Foreign Key and Navigation Property (Relationships)
            builder.HasOne(T => T.account)
                   .WithMany(A => A.transactions)
                   .HasForeignKey(T => T.accountNumber)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
