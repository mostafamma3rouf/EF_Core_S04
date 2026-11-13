using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_Core_S04.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranchesAndManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Code", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "BR-001", "12 Tahrir Square, Cairo", "Cairo Main Branch", "0225551001" },
                    { "BR-002", "45 Pyramids Road, Giza", "Giza Branch", "0233332002" },
                    { "BR-003", "8 Corniche Street, Alexandria", "Alexandria Branch", "0334443003" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FullName", "HireDate", "PhoneNumber", "branchCode" },
                values: new object[,]
                {
                    { 1, "ahmed.hassan@bank.com", "Ahmed Hassan", new DateTime(2018, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01011112222", "BR-001" },
                    { 2, "sara.mahmoud@bank.com", "Sara Mahmoud", new DateTime(2019, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "01122223333", "BR-002" },
                    { 3, "omar.khaled@bank.com", "Omar Khaled", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "01233334444", "BR-003" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "BR-001");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "BR-002");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "BR-003");
        }
    }
}
