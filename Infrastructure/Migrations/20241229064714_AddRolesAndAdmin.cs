using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreateDatetime", "CreatorUserId", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MobileNumber", "NationalCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdateDatetime", "UpdaterUserId", "UserName" },
                values: new object[] { new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), 0, "d967ee1a-2e8f-40dd-8d54-c66a750477b7", new DateTime(2024, 12, 29, 10, 17, 13, 564, DateTimeKind.Local).AddTicks(416), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mohamadreza@gmail.com", true, "محمدرضا", "امینی", false, null, "09111111111", "1234567890", "MOHAMADREZA@GMAIL.COM", "MOHAMADREZA@GMAIL.COM", "AQAAAAIAAYagAAAAEBDRwJ61I32XRL2hXgwavhyaPEKksrshLVQWBlMgu+W9RNaMXeQvJalv7HoqHYhfdA==", "88888888", false, "UYUOUWWKOLK3SIPRMHLRRAOSETBXTJVH", false, new DateTime(2024, 12, 29, 10, 17, 13, 564, DateTimeKind.Local).AddTicks(424), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), "mohamadreza@gmail.com" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreateDatetime", "CreatorUserId", "UpdateDatetime", "UpdaterUserId" },
                values: new object[] { new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(4362), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(4479), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreateDatetime", "CreatorUserId", "Name", "NormalizedName", "RoleDescription", "UpdateDatetime", "UpdaterUserId" },
                values: new object[,]
                {
                    { new Guid("6fe25e76-e02f-42d2-ac41-08dd25de10f8"), null, new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7202), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), "Customer", "CUSTOMER", "Customer products", new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7205), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") },
                    { new Guid("f9e1f0b3-a9c4-4eeb-ac40-08dd25de10f8"), null, new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7245), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), "Supplier", "SUPPLIER", "Seller of products", new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7246), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") },
                    { new Guid("fc961ce4-16c1-4566-ac3f-08dd25de10f8"), null, new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7250), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"), "Admin", "ADMIN", "Site Manager", new DateTime(2024, 12, 29, 10, 17, 13, 563, DateTimeKind.Local).AddTicks(7250), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("fc961ce4-16c1-4566-ac3f-08dd25de10f8"), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6fe25e76-e02f-42d2-ac41-08dd25de10f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f9e1f0b3-a9c4-4eeb-ac40-08dd25de10f8"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("fc961ce4-16c1-4566-ac3f-08dd25de10f8"), new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc961ce4-16c1-4566-ac3f-08dd25de10f8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d80d9352-0889-4b7a-ac71-2ae385ec05ca"));
        }
    }
}
