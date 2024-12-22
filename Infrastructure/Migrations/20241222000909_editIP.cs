using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editIP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "ViewLogs",
                type: "VarChar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VarChar",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "ViewLogs",
                type: "VarChar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VarChar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
