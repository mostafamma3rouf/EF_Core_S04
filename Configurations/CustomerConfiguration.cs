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
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Primary Key
            builder.HasKey(C => C.Id);

            // Properties (Columns)
            builder.Property(C => C.FullName).HasMaxLength(150);
            builder.Property(C => C.Email).HasMaxLength(250);
            builder.Property(C => C.PhoneNumber).HasMaxLength(50);
            builder.Property(C => C.Address).HasMaxLength(200);
            builder.Property(C => C.DateOfBirth);
            builder.Property(C => C.NationalId).HasMaxLength(50);
            builder.Property(C => C.CustomerType).HasMaxLength(50);
        }
    }
}
