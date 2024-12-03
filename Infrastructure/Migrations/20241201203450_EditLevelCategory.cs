using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditLevelCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StarRating",
                table: "Scores",
                type: "Int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "TinyInt",
                oldDefaultValue: (byte)0);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Scores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Images",
                type: "Int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TinyInt");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Categoryfeatures",
                type: "Int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TinyInt");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Categories",
                type: "Int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TinyInt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "StarRating",
                table: "Scores",
                type: "TinyInt",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(int),
                oldType: "Int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Scores",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<byte>(
                name: "Priority",
                table: "Images",
                type: "TinyInt",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "Int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<byte>(
                name: "Priority",
                table: "Categoryfeatures",
                type: "TinyInt",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "Int");

            migrationBuilder.AlterColumn<byte>(
                name: "Level",
                table: "Categories",
                type: "TinyInt",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "Int");
        }
    }
}
