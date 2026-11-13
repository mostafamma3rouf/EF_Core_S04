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
    internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Primary Key
            builder.HasKey(B => B.Code);

            // Properties (Columns)
            builder.Property(B => B.Code).HasMaxLength(50);
            builder.Property(B => B.Name).HasMaxLength(150);
            builder.Property(B => B.Address).HasMaxLength(200);
            builder.Property(B => B.PhoneNumber).HasMaxLength(50);

            // Seed Data
            builder.HasData(
            new Branch { Code = "BR-001", Name = "Cairo Main Branch", Address = "12 Tahrir Square, Cairo", PhoneNumber = "0225551001" },
            new Branch { Code = "BR-002", Name = "Giza Branch", Address = "45 Pyramids Road, Giza", PhoneNumber = "0233332002" },
            new Branch { Code = "BR-003", Name = "Alexandria Branch", Address = "8 Corniche Street, Alexandria", PhoneNumber = "0334443003" }
            );
        }
    }
}
