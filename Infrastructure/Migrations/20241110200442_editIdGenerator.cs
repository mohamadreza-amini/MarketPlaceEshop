using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editIdGenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UpdaterUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_CreatorUserId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UpdaterUserId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Admins_AdminConfirmedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatorUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UpdaterUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categoryfeatures_Admins_AdminConfirmedId",
                table: "Categoryfeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Categoryfeatures_AspNetUsers_CreatorUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Categoryfeatures_AspNetUsers_UpdaterUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Admins_AdminConfirmedId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_CreatorUserId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_UpdaterUserId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_Admins_AdminConfirmedId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_AspNetUsers_CreatorUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_AspNetUsers_UpdaterUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureValues_AdminConfirmedId",
                table: "ProductFeatureValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureValues_CreatorUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureValues_UpdaterUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropIndex(
                name: "IX_Images_AdminConfirmedId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CreatorUserId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UpdaterUserId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Categoryfeatures_AdminConfirmedId",
                table: "Categoryfeatures");

            migrationBuilder.DropIndex(
                name: "IX_Categoryfeatures_CreatorUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropIndex(
                name: "IX_Categoryfeatures_UpdaterUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AdminConfirmedId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatorUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdaterUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatorUserId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_UpdaterUserId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CreatorUserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UpdaterUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AdminConfirmedId",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "AdminConfirmedId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AdminConfirmedId",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "Categoryfeatures");

            migrationBuilder.DropColumn(
                name: "AdminConfirmedId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ConfirmedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdateDatetime",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdaterUserId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminConfirmedId",
                table: "ProductFeatureValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "ProductFeatureValues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "ProductFeatureValues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "ProductFeatureValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "IsConfirmed",
                table: "ProductFeatureValues",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "ProductFeatureValues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "ProductFeatureValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdminConfirmedId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "IsConfirmed",
                table: "Images",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdminConfirmedId",
                table: "Categoryfeatures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "Categoryfeatures",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "Categoryfeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Categoryfeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "IsConfirmed",
                table: "Categoryfeatures",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "Categoryfeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "Categoryfeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdminConfirmedId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "IsConfirmed",
                table: "Categories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDatetime",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDatetime",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterUserId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_AdminConfirmedId",
                table: "ProductFeatureValues",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_CreatorUserId",
                table: "ProductFeatureValues",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_UpdaterUserId",
                table: "ProductFeatureValues",
                column: "UpdaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdminConfirmedId",
                table: "Images",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CreatorUserId",
                table: "Images",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UpdaterUserId",
                table: "Images",
                column: "UpdaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoryfeatures_AdminConfirmedId",
                table: "Categoryfeatures",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoryfeatures_CreatorUserId",
                table: "Categoryfeatures",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoryfeatures_UpdaterUserId",
                table: "Categoryfeatures",
                column: "UpdaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AdminConfirmedId",
                table: "Categories",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorUserId",
                table: "Categories",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdaterUserId",
                table: "Categories",
                column: "UpdaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatorUserId",
                table: "Brands",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UpdaterUserId",
                table: "Brands",
                column: "UpdaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CreatorUserId",
                table: "Addresses",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UpdaterUserId",
                table: "Addresses",
                column: "UpdaterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorUserId",
                table: "Addresses",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UpdaterUserId",
                table: "Addresses",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_CreatorUserId",
                table: "Brands",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_UpdaterUserId",
                table: "Brands",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Admins_AdminConfirmedId",
                table: "Categories",
                column: "AdminConfirmedId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatorUserId",
                table: "Categories",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UpdaterUserId",
                table: "Categories",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoryfeatures_Admins_AdminConfirmedId",
                table: "Categoryfeatures",
                column: "AdminConfirmedId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoryfeatures_AspNetUsers_CreatorUserId",
                table: "Categoryfeatures",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoryfeatures_AspNetUsers_UpdaterUserId",
                table: "Categoryfeatures",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Admins_AdminConfirmedId",
                table: "Images",
                column: "AdminConfirmedId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_CreatorUserId",
                table: "Images",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_UpdaterUserId",
                table: "Images",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_Admins_AdminConfirmedId",
                table: "ProductFeatureValues",
                column: "AdminConfirmedId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_AspNetUsers_CreatorUserId",
                table: "ProductFeatureValues",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_AspNetUsers_UpdaterUserId",
                table: "ProductFeatureValues",
                column: "UpdaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
