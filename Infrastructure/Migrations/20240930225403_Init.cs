using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "NVarChar(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<string>(type: "VarChar(30)", maxLength: 30, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VarChar(30)", maxLength: 30, nullable: false),
                    MobileNumber = table.Column<string>(type: "VarChar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "NVarChar(100)", maxLength: 100, nullable: false),
                    NationalCode = table.Column<string>(type: "NVarChar(20)", maxLength: 20, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    RoleDescription = table.Column<string>(type: "VarChar(500)", maxLength: 500, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<byte>(type: "TinyInt", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "NVarChar(100)", maxLength: 100, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    ChairmanName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    CompanyRegistrationNumber = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Province = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Neighborhood = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    AddressDetail = table.Column<string>(type: "NVarChar(200)", maxLength: 200, nullable: false),
                    HouseNumber = table.Column<int>(type: "Int", nullable: false),
                    UnitNumber = table.Column<int>(type: "Int", nullable: false),
                    PostalCode = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categoryfeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<byte>(type: "TinyInt", nullable: false),
                    Filterable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoryfeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoryfeatures_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categoryfeatures_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titel = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "NVarChar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    View = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCost = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "NVarChar(30)", maxLength: 30, nullable: false),
                    Priority = table.Column<byte>(type: "TinyInt", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureValue = table.Column<string>(type: "NVarChar(500)", maxLength: 500, nullable: false),
                    CategoryFeatureId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatureValues_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductFeatureValues_Categoryfeatures_CategoryFeatureId",
                        column: x => x.CategoryFeatureId,
                        principalTable: "Categoryfeatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductFeatureValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ventory = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Discount = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0m),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductSuppliers_ProductSupplierId",
                        column: x => x.ProductSupplierId,
                        principalTable: "ProductSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentText = table.Column<string>(type: "NVarChar(2000)", maxLength: 2000, nullable: false),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_ProductSuppliers_ProductSupplierId",
                        column: x => x.ProductSupplierId,
                        principalTable: "ProductSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    UnitCost = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    UnitDiscount = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "Int", nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateOfPosting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductSuppliers_ProductSupplierId",
                        column: x => x.ProductSupplierId,
                        principalTable: "ProductSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceValue = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminConfirmedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Admins_AdminConfirmedId",
                        column: x => x.AdminConfirmedId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prices_ProductSuppliers_ProductSupplierId",
                        column: x => x.ProductSupplierId,
                        principalTable: "ProductSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StarRating = table.Column<byte>(type: "TinyInt", nullable: false, defaultValue: (byte)0),
                    ProductSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Scores_ProductSuppliers_ProductSupplierId",
                        column: x => x.ProductSupplierId,
                        principalTable: "ProductSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_MobileNumber",
                table: "Admins",
                column: "MobileNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_AdminConfirmedId",
                table: "Brands",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductSupplierId",
                table: "CartItems",
                column: "ProductSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AdminConfirmedId",
                table: "Categories",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoryfeatures_AdminConfirmedId",
                table: "Categoryfeatures",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoryfeatures_CategoryId",
                table: "Categoryfeatures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AdminConfirmedId",
                table: "Comments",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductSupplierId",
                table: "Comments",
                column: "ProductSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MobileNumber",
                table: "Customers",
                column: "MobileNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdminConfirmedId",
                table: "Images",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductSupplierId",
                table: "OrderItems",
                column: "ProductSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AdminConfirmedId",
                table: "Orders",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_AdminConfirmedId",
                table: "Prices",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductSupplierId",
                table: "Prices",
                column: "ProductSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_AdminConfirmedId",
                table: "ProductFeatureValues",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_CategoryFeatureId_ProductId",
                table: "ProductFeatureValues",
                columns: new[] { "CategoryFeatureId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_ProductId",
                table: "ProductFeatureValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AdminConfirmedId",
                table: "Products",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_ProductId",
                table: "ProductSuppliers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_SupplierId",
                table: "ProductSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_CustomerId_ProductSupplierId",
                table: "Scores",
                columns: new[] { "CustomerId", "ProductSupplierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ProductSupplierId",
                table: "Scores",
                column: "ProductSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AdminConfirmedId",
                table: "Suppliers",
                column: "AdminConfirmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_PhoneNumber",
                table: "Suppliers",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "ProductFeatureValues");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categoryfeatures");

            migrationBuilder.DropTable(
                name: "ProductSuppliers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
