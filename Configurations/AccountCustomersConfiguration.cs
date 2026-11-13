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
    internal class AccountCustomersConfiguration : IEntityTypeConfiguration<AccountCustomers>
    {
        public void Configure(EntityTypeBuilder<AccountCustomers> builder)
        {
            // Composite Key
            builder.HasKey(AC => new { AC.AccountNumber, AC.CustomerId});

            // Properties (Columns)
            builder.Property(AC => AC.OwnerShipStartDate);
            builder.Property(AC => AC.OwnerShipType).HasMaxLength(20);
            builder.Property(AC => AC.AccountStatus).HasMaxLength(20);

            // Foreign Key and Navigation Property (Relationships)
            builder.HasOne(AC => AC.account)
               .WithMany(A => A.accountCustomers)
               .HasForeignKey(AC => AC.AccountNumber)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(AC => AC.customer)
                   .WithMany(C => C.accountCustomers)
                   .HasForeignKey(AC => AC.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
