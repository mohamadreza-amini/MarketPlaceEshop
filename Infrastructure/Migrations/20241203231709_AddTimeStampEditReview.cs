using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeStampEditReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProductSuppliers_ProductSupplierId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_ProductSuppliers_ProductSupplierId",
                table: "Scores");

            migrationBuilder.RenameColumn(
                name: "ProductSupplierId",
                table: "Scores",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_ProductSupplierId",
                table: "Scores",
                newName: "IX_Scores_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_CustomerId_ProductSupplierId",
                table: "Scores",
                newName: "IX_Scores_CustomerId_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductSupplierId",
                table: "Comments",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductSupplierId",
                table: "Comments",
                newName: "IX_Comments_ProductId");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProductSuppliers",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Products_ProductId",
                table: "Scores",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Products_ProductId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProductSuppliers");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Scores",
                newName: "ProductSupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_ProductId",
                table: "Scores",
                newName: "IX_Scores_ProductSupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_CustomerId_ProductId",
                table: "Scores",
                newName: "IX_Scores_CustomerId_ProductSupplierId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comments",
                newName: "ProductSupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                newName: "IX_Comments_ProductSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProductSuppliers_ProductSupplierId",
                table: "Comments",
                column: "ProductSupplierId",
                principalTable: "ProductSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_ProductSuppliers_ProductSupplierId",
                table: "Scores",
                column: "ProductSupplierId",
                principalTable: "ProductSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
