using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Admins_AdminConfirmedId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_AdminConfirmedId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "AdminConfirmedId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminConfirmedId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "Brands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "IsConfirmed",
                table: "Brands",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_AdminConfirmedId",
                table: "Brands",
                column: "AdminConfirmedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Admins_AdminConfirmedId",
                table: "Brands",
                column: "AdminConfirmedId",
                principalTable: "Admins",
                principalColumn: "Id");
        }
    }
}
