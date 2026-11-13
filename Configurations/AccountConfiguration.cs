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
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Primary Key
            builder.HasKey(A => A.AccountNumber);

            // Properties (Columns)
            builder.Property(A => A.AccountNumber).HasMaxLength(50);
            builder.Property(A => A.CurrentBalance).HasPrecision(18, 2);
            builder.Property(A => A.AccountType).HasMaxLength(50);
            builder.Property(A => A.OpeningDate);

            // Foreign Key and Navigation Property (Relationships)
            builder.HasOne(A => A.branch)
                   .WithMany(B => B.accounts)
                   .HasForeignKey(A => A.branchCode)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
