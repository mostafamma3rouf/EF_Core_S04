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
    internal class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            // Primary Key
            builder.HasKey(M => M.Id);

            // Properties (Columns)
            builder.Property(M => M.FullName).HasMaxLength(150);
            builder.Property(M => M.Email).HasMaxLength(250);
            builder.Property(M => M.PhoneNumber).HasMaxLength(50);
            builder.Property(M => M.HireDate);

            // Foreign Key and Navigation Property (Relationships)
            builder.HasOne(M => M.branch)
                   .WithOne(B => B.manager)
                   .HasForeignKey<Manager>(M => M.branchCode)
                   .OnDelete(DeleteBehavior.Restrict);

            // Seed Data
            builder.HasData(
            new Manager { Id = 1, FullName = "Ahmed Hassan", Email = "ahmed.hassan@bank.com", PhoneNumber = "01011112222", HireDate = new DateTime(2018, 3, 1), branchCode = "BR-001" },
            new Manager { Id = 2, FullName = "Sara Mahmoud", Email = "sara.mahmoud@bank.com", PhoneNumber = "01122223333", HireDate = new DateTime(2019, 7, 15), branchCode = "BR-002" },
            new Manager { Id = 3, FullName = "Omar Khaled", Email = "omar.khaled@bank.com", PhoneNumber = "01233334444", HireDate = new DateTime(2020, 1, 10), branchCode = "BR-003" }
            );
        }
    }
}
