using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechShop.Core.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LoyaltyPoints = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    MethodPaymment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    CollaboratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    UserReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Comments_UserReplyId",
                        column: x => x.UserReplyId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHardwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHardwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHardwares_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filters_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Collaborators_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collaborators_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductHardwareId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RGB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColors_ProductHardwares_ProductHardwareId",
                        column: x => x.ProductHardwareId,
                        principalTable: "ProductHardwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFilters",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFilters", x => new { x.FilterId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductFilters_Filters_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFilters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollaboratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Chats_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProducts",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductColorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProducts", x => new { x.InvoiceId, x.ProductColorsId });
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_ProductColors_ProductColorsId",
                        column: x => x.ProductColorsId,
                        principalTable: "ProductColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quanitity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderProductColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.CustomerId, x.ProductColorId });
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Orders_OrderCustomerId_OrderProductColorId",
                        columns: x => new { x.OrderCustomerId, x.OrderProductColorId },
                        principalTable: "Orders",
                        principalColumns: new[] { "CustomerId", "ProductColorId" });
                    table.ForeignKey(
                        name: "FK_Orders_ProductColors_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProductColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCustomer = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2492a74d-3c9f-4a27-9e21-9783ebeb8ee1", null, "Collaborator", "COLLABORATOR" },
                    { "35876399-17ca-49a4-adbf-679932581012", null, "Admin", "ADMIN" },
                    { "cdad2dad-54da-4ba6-b06a-e787fd18685a", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDay", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "7954dd0b-fc37-488f-9f9b-cf64179fa2d9", 0, "202 Oak St, Miami, FL", new DateTime(2003, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "90e3aa2a-8269-4e81-888b-935d51c77c3b", new DateTime(2024, 9, 26, 9, 41, 52, 904, DateTimeKind.Local).AddTicks(3977), "duclt24@fpt.com", false, "Le Toan Duc", false, null, null, null, "AQAAAAIAAYagAAAAEC5MU6wKTX3m98sXSxIxFnzfDYGiQoDASllYspCBrSPcBSmJmhVjN5Vl0OBKnI2tfA==", "3335678901", false, "52e9bc06-f580-4d9c-85dc-e60cb9db39d9", false, new DateTime(2024, 9, 26, 9, 41, 52, 904, DateTimeKind.Local).AddTicks(3987), "duclt24" },
                    { "bdf2f396-543a-4d69-ad81-e346218aec1e", 0, "123 Main St, New York, NY", new DateTime(2001, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "f565f0da-1fba-45e3-bc5d-8c6f3f27806f", new DateTime(2024, 9, 26, 9, 41, 52, 556, DateTimeKind.Local).AddTicks(6881), "phucnd20@fpt.com", false, "Nguyen Duy Phuc", false, null, null, null, "AQAAAAIAAYagAAAAEJoxephNHqTUVBCmL4WoFY/7eJIRjZx23ilIJiRbvOXZZ/Y57KMTot+QvBZMqTSfRg==", "1234567890", false, "6bdcc829-06c9-4340-bacd-100ccfbbd1b6", false, new DateTime(2024, 9, 26, 9, 41, 52, 556, DateTimeKind.Local).AddTicks(6890), "phucnd20" },
                    { "dfd365b0-ed6c-4627-8d91-6f6488d301b1", 0, "101 Maple St, San Francisco, CA", new DateTime(2002, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "dbb00183-195f-4d8a-a9bf-d9cab6ef1be2", new DateTime(2024, 9, 26, 9, 41, 52, 830, DateTimeKind.Local).AddTicks(4092), "nhannt96@fpt.com", false, "Nguyen Tri Nhan", false, null, null, null, "AQAAAAIAAYagAAAAEGWgQ+WzO73fDEevdsJtBgcp/3iGEy7f/B6z2L/3LTlHCifDgWDryedBKF15uJ8IBA==", "4449876543", false, "5e8160fc-de5a-42db-aacc-3f5dca9881ae", false, new DateTime(2024, 9, 26, 9, 41, 52, 830, DateTimeKind.Local).AddTicks(4100), "nhannt96" },
                    { "dfda172b-27f8-44d5-aef4-d029719eeab7", 0, "789 Pine St, Chicago, IL", new DateTime(2003, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "4eece4b4-79a3-4b86-b5f3-e9da05259f56", new DateTime(2024, 9, 26, 9, 41, 52, 755, DateTimeKind.Local).AddTicks(2207), "dattv20@fpt.com", false, "Tran Van Dat", false, null, null, null, "AQAAAAIAAYagAAAAENAcMywUzVdU2xVMkv+pJZl24vDOWbvdOBHFjsGjJpnL1j3HPgfQy6aQ62eIa5CA9w==", "5551234567", false, "30e6e46d-ab7f-47d7-93ce-484935c2d014", false, new DateTime(2024, 9, 26, 9, 41, 52, 755, DateTimeKind.Local).AddTicks(2214), "dattv20" },
                    { "e1c5db55-de77-41c1-ab45-c022d32a2034", 0, "456 Elm St, Los Angeles, CA", new DateTime(2003, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "74a1e39c-3d36-4ff4-beec-4bc56b519849", new DateTime(2024, 9, 26, 9, 41, 52, 667, DateTimeKind.Local).AddTicks(604), "khanhtmq2@fpt.com", false, "Tran Minh Quoc Khanh", false, null, null, null, "AQAAAAIAAYagAAAAEBBwhUyD9JMwUGvdm8Gfw1AaLxtgn5wZoCaajCY8vEfoEP0JkY6mxqAoocU5TjmLJw==", "9876543210", false, "a7e55e9d-7178-4231-8897-245dd7909550", false, new DateTime(2024, 9, 26, 9, 41, 52, 667, DateTimeKind.Local).AddTicks(609), "khanhtmq2" },
                    { "f7ab9962-24e9-43fa-b81d-af09531b6048", 0, "707 Cherry St, Phoenix, AZ", new DateTime(2002, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "dd28d1c2-5682-4687-96c1-058ddab94d59", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(7795), "anhbhn@fpt.com", false, "Bui Huynh Ngoc Anh", false, null, null, null, "AQAAAAIAAYagAAAAEBHMpwm80JxgWKVBJ+Ua1zM5q7kV5XiWnYktSlPwau3smXhDMPSegMxgRlUOkBeWLA==", "8886785432", false, "ad60eae5-e262-4d26-be93-62351f3801ec", false, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(7805), "anhbhn" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("210eb0ed-23fd-42f8-9dd7-b00cfd19715e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8793), null, "Asus", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8794), "asus" },
                    { new Guid("48720627-137e-4089-9668-f8f3b823186a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8778), null, "Dell", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8779), "dell" },
                    { new Guid("6153d1df-8e4c-4f10-ab8e-a60809573c05"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8775), null, "Sony", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8776), "sony" },
                    { new Guid("7a0f7330-2fe2-432d-91c1-ee96a48a8ca8"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8768), null, "Apple", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8769), "apple" },
                    { new Guid("85e9a56d-fc9b-4e1f-ad68-21cca7dfb436"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8772), null, "Samsung", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8772), "samsung" },
                    { new Guid("a0afc49f-f2c1-4c04-8916-4e05b6ac7fe0"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8781), null, "HP", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8782), "hp" },
                    { new Guid("c86a7099-dea4-46b2-9017-12bc015d23e4"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8789), null, "Microsoft", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8791), "microsoft" },
                    { new Guid("ed7da3d4-b839-4c35-ae1c-663760a9794d"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8796), null, "Acer", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8797), "acer" },
                    { new Guid("efd5a997-0a4f-457a-9969-df088d01c82e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8787), null, "Lenovo", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8787), "lenovo" },
                    { new Guid("fe9d1df6-9f06-4436-b52f-bc821466c09a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8800), null, "Huawei", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8801), "huawei" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("09e18647-e4e5-4eb2-8b47-cf0d8802eafd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8753), null, "Accessories", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8753), "accessories" },
                    { new Guid("2f17b91e-1f82-4720-8f32-c972ec3c8a09"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8713), null, "Tablets", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8713), "tablets" },
                    { new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8689), null, "Phones", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8692), "phones" },
                    { new Guid("76f25b28-3096-4a32-9a3a-5646b720015f"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8723), null, "PCs", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8735), "pcs" },
                    { new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8696), null, "Laptops", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8696), "laptops" },
                    { new Guid("908d467a-84ca-4b7f-a37c-d9964bd3d788"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8720), null, "Watches", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8721), "watches" },
                    { new Guid("ad295af0-70e4-4a63-8e37-eeb960e8998e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8742), null, "TVs", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8743), "tvs" },
                    { new Guid("aff23a31-95a1-4f59-8d71-7d99aa9eb71a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8756), null, "Audio", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8756), "audio" },
                    { new Guid("dbc5b522-1516-4469-a7a1-b42e2637b3cd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8758), null, "Cameras", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8759), "cameras" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "AccessLevel", "UserID" },
                values: new object[] { new Guid("998593d3-4b26-4599-b074-e6ffbb3bc029"), 2, "bdf2f396-543a-4d69-ad81-e346218aec1e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2492a74d-3c9f-4a27-9e21-9783ebeb8ee1", "7954dd0b-fc37-488f-9f9b-cf64179fa2d9" },
                    { "35876399-17ca-49a4-adbf-679932581012", "bdf2f396-543a-4d69-ad81-e346218aec1e" },
                    { "2492a74d-3c9f-4a27-9e21-9783ebeb8ee1", "dfd365b0-ed6c-4627-8d91-6f6488d301b1" },
                    { "2492a74d-3c9f-4a27-9e21-9783ebeb8ee1", "dfda172b-27f8-44d5-aef4-d029719eeab7" },
                    { "2492a74d-3c9f-4a27-9e21-9783ebeb8ee1", "e1c5db55-de77-41c1-ab45-c022d32a2034" },
                    { "cdad2dad-54da-4ba6-b06a-e787fd18685a", "f7ab9962-24e9-43fa-b81d-af09531b6048" }
                });

            migrationBuilder.InsertData(
                table: "Collaborators",
                columns: new[] { "ID", "EndDate", "InvoiceId", "StartDate", "UserID" },
                values: new object[,]
                {
                    { new Guid("1e4a2c6e-1051-4122-8093-de12dd13f518"), new DateTime(2025, 2, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8645), null, new DateTime(2024, 7, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8644), "7954dd0b-fc37-488f-9f9b-cf64179fa2d9" },
                    { new Guid("28e0105a-3dbe-45c1-8bdf-332755f6e704"), new DateTime(2025, 2, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8628), null, new DateTime(2024, 4, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8611), "e1c5db55-de77-41c1-ab45-c022d32a2034" },
                    { new Guid("841c0bd9-a823-4fbc-98eb-28b4c50c534b"), new DateTime(2025, 2, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8641), null, new DateTime(2024, 7, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8639), "dfd365b0-ed6c-4627-8d91-6f6488d301b1" },
                    { new Guid("cf4d2168-28f5-489d-ab2a-61c4a9937f81"), new DateTime(2025, 2, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8636), null, new DateTime(2024, 6, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8634), "dfda172b-27f8-44d5-aef4-d029719eeab7" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "CreatedAt", "LoyaltyPoints", "UpdatedAt", "UserID" },
                values: new object[] { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), null, 150, null, "f7ab9962-24e9-43fa-b81d-af09531b6048" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "Description", "Discount", "Image", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("05553658-96ea-4fbe-bcdd-9bcb60714da6"), new Guid("7a0f7330-2fe2-432d-91c1-ee96a48a8ca8"), new Guid("09e18647-e4e5-4eb2-8b47-cf0d8802eafd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9025), "Wireless earbuds with active noise cancellation.", 5, "https://example.com/images/airpods-pro.jpg", "Apple AirPods Pro", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9026), "airpods-pro" },
                    { new Guid("1c2fb759-8ee2-40eb-942b-15cfc5ffe140"), new Guid("a0afc49f-f2c1-4c04-8916-4e05b6ac7fe0"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9042), "Sleek laptop with powerful Intel i7 processor.", 12, "https://example.com/images/hp-envy15.jpg", "HP Envy 15", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9042), "hp-envy15" },
                    { new Guid("2807a392-5d67-4bf8-ad60-619cbd15b389"), new Guid("ed7da3d4-b839-4c35-ae1c-663760a9794d"), new Guid("76f25b28-3096-4a32-9a3a-5646b720015f"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9014), "Compact gaming desktop with powerful performance.", 20, "https://example.com/images/acer-predator.jpg", "Acer Predator Orion 3000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9015), "acer-predator-orion" },
                    { new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), new Guid("85e9a56d-fc9b-4e1f-ad68-21cca7dfb436"), new Guid("908d467a-84ca-4b7f-a37c-d9964bd3d788"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8830), "Smartwatch with advanced health tracking features.", 12, "https://example.com/images/galaxy-watch.jpg", "Samsung Galaxy Watch", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8831), "galaxy-watch" },
                    { new Guid("495a42be-85fd-452c-abfc-55edead730af"), new Guid("85e9a56d-fc9b-4e1f-ad68-21cca7dfb436"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8818), "Flagship Samsung phone with Exynos 2100.", 10, "https://example.com/images/galaxy-s21.jpg", "Samsung Galaxy S21", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8819), "galaxy-s21" },
                    { new Guid("5f587109-15d8-4b5a-af8a-d18c50cf1073"), new Guid("efd5a997-0a4f-457a-9969-df088d01c82e"), new Guid("2f17b91e-1f82-4720-8f32-c972ec3c8a09"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9046), "Versatile tablet with built-in kickstand.", 7, "https://example.com/images/lenovo-yoga.jpg", "Lenovo Yoga Smart Tab", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9046), "lenovo-yoga-smart-tab" },
                    { new Guid("64ca27c2-5911-45ef-85bf-3bddd4f1284b"), new Guid("ed7da3d4-b839-4c35-ae1c-663760a9794d"), new Guid("aff23a31-95a1-4f59-8d71-7d99aa9eb71a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9060), "Convertible laptop with a sleek design.", 20, "https://example.com/images/acer-spin5.jpg", "Acer Spin 5", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9061), "acer-spin5" },
                    { new Guid("7364d009-fb9d-4ac8-904f-fb8839dd6628"), new Guid("6153d1df-8e4c-4f10-ab8e-a60809573c05"), new Guid("dbc5b522-1516-4469-a7a1-b42e2637b3cd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9034), "Industry-leading noise canceling headphones.", 12, "https://example.com/images/sony-wh1000xm4.jpg", "Sony WH-1000XM4", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9034), "sony-wh1000xm4" },
                    { new Guid("7466bf1b-6888-4d9c-a83b-697a76f82ee3"), new Guid("c86a7099-dea4-46b2-9017-12bc015d23e4"), new Guid("dbc5b522-1516-4469-a7a1-b42e2637b3cd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9000), "Next-gen gaming console with 4K gaming capabilities.", 5, "https://example.com/images/xbox-series-x.jpg", "Microsoft Xbox Series X", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9004), "xbox-series-x" },
                    { new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38"), new Guid("48720627-137e-4089-9668-f8f3b823186a"), new Guid("dbc5b522-1516-4469-a7a1-b42e2637b3cd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9038), "Top-tier gaming desktop with cutting-edge hardware.", 18, "https://example.com/images/alienware.jpg", "Dell Alienware", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9038), "dell-alienware" },
                    { new Guid("76a1caa8-5d92-4dfa-83e2-09beed5af447"), new Guid("210eb0ed-23fd-42f8-9dd7-b00cfd19715e"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9010), "Ultra-thin gaming laptop with AMD Ryzen 9 processor.", 15, "https://example.com/images/asus-rog.jpg", "Asus ROG Zephyrus", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9011), "asus-rog-zephyrus" },
                    { new Guid("787ec87d-d82d-4bb9-ba3c-517e7e60d16f"), new Guid("fe9d1df6-9f06-4436-b52f-bc821466c09a"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9021), "Flagship Huawei smartphone with cutting-edge technology.", 10, "https://example.com/images/huawei-mate40.jpg", "Huawei Mate 40 Pro", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9021), "huawei-mate40-pro" },
                    { new Guid("826f3115-6208-4188-b05e-c23e0f095cc4"), new Guid("210eb0ed-23fd-42f8-9dd7-b00cfd19715e"), new Guid("908d467a-84ca-4b7f-a37c-d9964bd3d788"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9057), "Stylish smartwatch with customizable faces.", 15, "https://example.com/images/asus-zenwatch3.jpg", "Asus ZenWatch 3", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9057), "asus-zenwatch3" },
                    { new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), new Guid("48720627-137e-4089-9668-f8f3b823186a"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8988), "Compact and powerful ultrabook with Intel i7 processor.", 10, "https://example.com/images/dell-xps13.jpg", "Dell XPS 13", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8989), "dell-xps13" },
                    { new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), new Guid("7a0f7330-2fe2-432d-91c1-ee96a48a8ca8"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8811), "Latest Apple iPhone with A15 Bionic chip.", 5, "https://example.com/images/iphone13.jpg", "iPhone 13", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8813), "iphone-13" },
                    { new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00"), new Guid("efd5a997-0a4f-457a-9969-df088d01c82e"), new Guid("dbc5b522-1516-4469-a7a1-b42e2637b3cd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8997), "High-performance gaming laptop with NVIDIA RTX graphics.", 18, "https://example.com/images/lenovo-legion5.jpg", "Lenovo Legion 5", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8997), "lenovo-legion5" },
                    { new Guid("c3686570-7258-447e-8495-f776f2298ee0"), new Guid("85e9a56d-fc9b-4e1f-ad68-21cca7dfb436"), new Guid("aff23a31-95a1-4f59-8d71-7d99aa9eb71a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9029), "True wireless earbuds with premium sound.", 8, "https://example.com/images/galaxy-buds.jpg", "Samsung Galaxy Buds Live", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9030), "galaxy-buds-live" },
                    { new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), new Guid("7a0f7330-2fe2-432d-91c1-ee96a48a8ca8"), new Guid("2f17b91e-1f82-4720-8f32-c972ec3c8a09"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8822), "Apple iPad Pro with M1 chip.", 7, "https://example.com/images/ipad-pro.jpg", "iPad Pro", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8823), "ipad-pro" },
                    { new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc"), new Guid("a0afc49f-f2c1-4c04-8916-4e05b6ac7fe0"), new Guid("76f25b28-3096-4a32-9a3a-5646b720015f"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8993), "Affordable desktop with AMD Ryzen processor.", 20, "https://example.com/images/hp-pavilion.jpg", "HP Pavilion Desktop", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8993), "hp-pavilion" },
                    { new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), new Guid("6153d1df-8e4c-4f10-ab8e-a60809573c05"), new Guid("ad295af0-70e4-4a63-8e37-eeb960e8998e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8838), "4K UHD Smart TV with OLED display.", 15, "https://example.com/images/sony-bravia.jpg", "Sony Bravia 55-inch", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(8839), "sony-bravia" },
                    { new Guid("e8248af7-dded-446e-bcd4-de817024becf"), new Guid("c86a7099-dea4-46b2-9017-12bc015d23e4"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9050), "Dual-screen device for productivity on the go.", 10, "https://example.com/images/surface-duo.jpg", "Microsoft Surface Duo", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9050), "microsoft-surface-duo" }
                });

            migrationBuilder.InsertData(
                table: "Specifications",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), "RAM" },
                    { new Guid("15e46894-51de-4a26-a7db-346c8c4e8ecd"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), "Chip" },
                    { new Guid("2c2f27b1-6414-42f0-88ec-368551753894"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), "Storage" },
                    { new Guid("4a692d63-5a1a-45fe-98f9-684cbbb627b1"), new Guid("76f25b28-3096-4a32-9a3a-5646b720015f"), "Processor" },
                    { new Guid("74b3b3a3-c8f1-4282-8e7e-7982269e1c18"), new Guid("ad295af0-70e4-4a63-8e37-eeb960e8998e"), "Screen Size" },
                    { new Guid("763e8516-381a-4f94-91d2-77f40daa6758"), new Guid("76f25b28-3096-4a32-9a3a-5646b720015f"), "Graphic Card" },
                    { new Guid("da651453-9c68-45c0-9d8b-010c045c8c4e"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), "RAM" },
                    { new Guid("efce27b0-1d81-4cd6-b856-e0a283a6196a"), new Guid("ad295af0-70e4-4a63-8e37-eeb960e8998e"), "Resolution" }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "CollaboratorId", "CreatedAt", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new Guid("28e0105a-3dbe-45c1-8bdf-332755f6e704"), new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(183), new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849") },
                    { new Guid("70d16e23-5df5-486e-8e68-e49aa8550a9c"), new Guid("cf4d2168-28f5-489d-ab2a-61c4a9937f81"), new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(189), new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentText", "CreatedAt", "ProductId", "Rate", "UpdatedAt", "UserId", "UserReplyId" },
                values: new object[,]
                {
                    { new Guid("3fac9b73-305e-43db-91c1-bc98df10e5e4"), "Not worth the price.", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9804), new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), 2, null, "dfd365b0-ed6c-4627-8d91-6f6488d301b1", null },
                    { new Guid("602d79c7-6d77-4c8e-9fb1-64d4b128bbb7"), "Great customer service.", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9807), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), 4, null, "bdf2f396-543a-4d69-ad81-e346218aec1e", null },
                    { new Guid("68d210b6-988f-4841-8f77-7e624d87d439"), "Not good", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9792), new Guid("495a42be-85fd-452c-abfc-55edead730af"), 3, null, "e1c5db55-de77-41c1-ab45-c022d32a2034", null },
                    { new Guid("79ec51b2-f376-4be3-8f18-5fa0e56f6662"), "The product arrived damaged.", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9811), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), 1, null, "e1c5db55-de77-41c1-ab45-c022d32a2034", null },
                    { new Guid("a64613fe-8ff4-4339-8964-5ef3e36e876c"), "Good", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9787), new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), 5, null, "bdf2f396-543a-4d69-ad81-e346218aec1e", null },
                    { new Guid("fd08bbf6-1d51-41e6-a2fd-1ba7fe1d4807"), "Excellent value for money!", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9801), new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), 5, null, "dfda172b-27f8-44d5-aef4-d029719eeab7", null }
                });

            migrationBuilder.InsertData(
                table: "Filters",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Name", "SpecificationId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("37d35afd-167d-4be2-915d-80074b52c4b8"), new Guid("ad295af0-70e4-4a63-8e37-eeb960e8998e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9692), "4K UHD", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9693) },
                    { new Guid("3c4265fb-955d-4e2d-9d7f-2f40ca0164ff"), new Guid("908d467a-84ca-4b7f-a37c-d9964bd3d788"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9684), "Water Resistant", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9685) },
                    { new Guid("54794ede-52fb-46fb-b35f-9285d51338eb"), new Guid("2f17b91e-1f82-4720-8f32-c972ec3c8a09"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9677), "OLED Display", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9677) },
                    { new Guid("5a6e4e4e-f8ce-46f7-b5e2-7e8968ce43ed"), new Guid("908d467a-84ca-4b7f-a37c-d9964bd3d788"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9687), "Heart Rate Monitor", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9688) },
                    { new Guid("67507f8c-2351-48b7-b07e-6b50b69fb7db"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9660), "4GB RAM", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9660) },
                    { new Guid("894f8507-cf37-4d63-9c16-993b543654a6"), new Guid("6d0dfc2c-f26f-4ec4-aab7-59079e5fe7c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9664), "128GB Storage", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9664) },
                    { new Guid("90abdddb-d6a9-414f-b40d-70e1756a0b83"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9667), "8GB RAM", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9668) },
                    { new Guid("a94775da-e452-4270-9a05-537f103cd70b"), new Guid("2f17b91e-1f82-4720-8f32-c972ec3c8a09"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9680), "5G Connectivity", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9681) },
                    { new Guid("fa45c0be-014d-4e41-bbd4-4fc7bec97358"), new Guid("80ff6f18-ea57-43f3-9b8d-226a4b002758"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9673), "256GB Storage", new Guid("140cb12c-ba27-4e99-8d60-319711f1ecf1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9674) }
                });

            migrationBuilder.InsertData(
                table: "ProductHardwares",
                columns: new[] { "Id", "CreatedAt", "Name", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("034b5ec2-bb45-4a90-a33c-7e67405ba90e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9180), "Intel i7 Processor", new Guid("1c2fb759-8ee2-40eb-942b-15cfc5ffe140"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9180) },
                    { new Guid("08631063-36cb-4f23-b023-667083f58472"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9165), "Active Noise Cancellation", new Guid("c3686570-7258-447e-8495-f776f2298ee0"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9166) },
                    { new Guid("0b7a6fa1-7b24-410a-92ae-5fdd31f811e9"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9149), "Intel i9 Processor", new Guid("2807a392-5d67-4bf8-ad60-619cbd15b389"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9149) },
                    { new Guid("13d20162-6371-43bd-b866-21224b0fb437"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9103), "4K UHD", new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9103) },
                    { new Guid("19cab37f-64da-43a3-8fc2-1f9df6e7b56a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9186), "128GB Storage", new Guid("e8248af7-dded-446e-bcd4-de817024becf"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9186) },
                    { new Guid("2dd54462-a38b-4435-a06b-93faa9b4c2ec"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9100), "LTE Version", new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9100) },
                    { new Guid("45d20a4c-862d-4824-bf96-51040aed1538"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9177), "32GB RAM", new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9177) },
                    { new Guid("4bc53e12-65a6-4699-a5ce-db60c5a29319"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9096), "Bluetooth Version", new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9097) },
                    { new Guid("4be38d4a-28fc-4a20-a4dd-af0e3bff924a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9106), "Intel i7 Processor", new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9107) },
                    { new Guid("506f96f7-044e-4eae-844f-cd7e9be27c7a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9146), "32GB RAM", new Guid("76a1caa8-5d92-4dfa-83e2-09beed5af447"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9147) },
                    { new Guid("584ff42a-80f8-4d4d-b1d5-6dd59d7fff54"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9192), "Convertible Design", new Guid("64ca27c2-5911-45ef-85bf-3bddd4f1284b"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9193) },
                    { new Guid("65354317-24ef-4f8a-b16c-7290837603da"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9138), "1TB Storage", new Guid("7466bf1b-6888-4d9c-a83b-697a76f82ee3"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9139) },
                    { new Guid("6f00ed86-5402-4cab-8cca-678a9df06ea1"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9162), "Wireless Charging Case", new Guid("05553658-96ea-4fbe-bcdd-9bcb60714da6"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9162) },
                    { new Guid("73938925-bf83-49d8-a08d-9abb037ab75a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9183), "128GB Storage", new Guid("5f587109-15d8-4b5a-af8a-d18c50cf1073"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9183) },
                    { new Guid("83207d05-25da-476b-b957-1df38ce55840"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9091), "256GB Storage", new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9092) },
                    { new Guid("8808db8a-325a-4710-bb8f-95f5cb9a2891"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9142), "AMD Ryzen 9 Processor", new Guid("76a1caa8-5d92-4dfa-83e2-09beed5af447"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9143) },
                    { new Guid("8a8baa65-294c-4e58-89e2-cf81359c2d93"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9084), "256GB Storage", new Guid("495a42be-85fd-452c-abfc-55edead730af"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9084) },
                    { new Guid("947abebf-e343-4d27-b46f-81471b5d204b"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9174), "Intel i9 Processor", new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9174) },
                    { new Guid("a241ff16-4d75-4880-b0ee-5807284ecd45"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9087), "128GB Storage", new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9087) },
                    { new Guid("adbb661f-a09e-4646-be50-bbb6423168b4"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9171), "Industry-leading Noise Cancellation", new Guid("7364d009-fb9d-4ac8-904f-fb8839dd6628"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9171) },
                    { new Guid("b0018fde-172d-412e-bfd4-e7497e18f848"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9120), "NVIDIA RTX 3060", new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9121) },
                    { new Guid("b5de408d-358f-423a-9078-db785a9a41ac"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9130), "16GB RAM", new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9131) },
                    { new Guid("b9528a02-76f2-4314-9f78-bba0ea096e51"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9116), "8GB RAM", new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9117) },
                    { new Guid("d748131c-d6b3-46c9-971f-ac84564a9c7b"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9188), "Customizable Watch Faces", new Guid("826f3115-6208-4188-b05e-c23e0f095cc4"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9189) },
                    { new Guid("d9035c57-39a4-4c94-b35a-469899e39d62"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9072), "256GB Storage", new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9073) },
                    { new Guid("dc71e128-e94c-4f43-9739-bee2b9baf820"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9080), "128GB Storage", new Guid("495a42be-85fd-452c-abfc-55edead730af"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9081) },
                    { new Guid("efb16b2d-65f4-4827-a8aa-fda6219240d4"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9152), "256GB Storage", new Guid("787ec87d-d82d-4bb9-ba3c-517e7e60d16f"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9152) },
                    { new Guid("efb9d89c-d4c1-4bd7-b82d-ceb9d57edc6a"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9113), "AMD Ryzen 5 Processor", new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9113) },
                    { new Guid("f3e62faf-2956-4667-9501-2e94435e0c44"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9110), "16GB RAM", new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9110) },
                    { new Guid("fc23ca85-6bf4-4821-b1ca-c19478e12f17"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9077), "64GB Storage", new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9078) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Alt", "CreatedAt", "ProductId", "Title", "Type", "UpdatedAt", "UrlImage" },
                values: new object[,]
                {
                    { new Guid("0ada76f0-1873-4cdc-9337-3ee59a1bc961"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9321), new Guid("7466bf1b-6888-4d9c-a83b-697a76f82ee3"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9322), "https://example.com/images/xbox-series-x-content.jpg" },
                    { new Guid("0d59b700-1f34-4dd0-be92-52f03c36fbde"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9382), new Guid("1c2fb759-8ee2-40eb-942b-15cfc5ffe140"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9382), "https://example.com/images/hp-envy15-content.jpg" },
                    { new Guid("23eb2151-fd71-4ea5-830a-03e193c2efa9"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9354), new Guid("c3686570-7258-447e-8495-f776f2298ee0"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9355), "https://example.com/images/galaxy-buds-slide1.jpg" },
                    { new Guid("2e444beb-0df4-414d-9414-02e21ecd4b12"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9372), new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9372), "https://example.com/images/alienware-content.jpg" },
                    { new Guid("34b7889e-a16d-486e-b174-732d00faac6b"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9296), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9297), "https://example.com/images/dell-xps13-content.jpg" },
                    { new Guid("367ecfe6-5083-4faf-a18f-298b683a25c3"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9358), new Guid("c3686570-7258-447e-8495-f776f2298ee0"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9359), "https://example.com/images/galaxy-buds-content.jpg" },
                    { new Guid("372aafa5-11c2-4818-9d26-4d85065027cd"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9290), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9290), "https://example.com/images/sony-bravia-content.jpg" },
                    { new Guid("43bd5d87-0a37-43c3-a4a0-da0802f24d65"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9342), new Guid("787ec87d-d82d-4bb9-ba3c-517e7e60d16f"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9343), "https://example.com/images/huawei-mate40-content.jpg" },
                    { new Guid("46521dca-5860-4bdc-b160-4eb1bd3b5196"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9311), new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9312), "https://example.com/images/lenovo-legion5-content.jpg" },
                    { new Guid("4bdb3a37-ac57-421e-b2c8-8d6b672a7237"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9300), new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9301), "https://example.com/images/hp-pavilion-slide1.jpg" },
                    { new Guid("4e0cc3ec-09ff-41a8-915f-16d0aecf0817"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9394), new Guid("e8248af7-dded-446e-bcd4-de817024becf"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9395), "https://example.com/images/apple-tv4k-slide1.jpg" },
                    { new Guid("5a67f631-c73e-44d1-bc16-224c63a7cf68"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9335), new Guid("2807a392-5d67-4bf8-ad60-619cbd15b389"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9335), "https://example.com/images/acer-predator-content.jpg" },
                    { new Guid("60729a74-580a-448e-b32d-08e8e904ffea"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9305), new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9305), "https://example.com/images/hp-pavilion-content.jpg" },
                    { new Guid("6760223f-44cd-4725-9c13-e9d7f3c84371"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9204), new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9205), "https://example.com/images/iphone13-slide1.jpg" },
                    { new Guid("7004cae8-62c1-46f7-8778-2776ac1b8a44"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9324), new Guid("76a1caa8-5d92-4dfa-83e2-09beed5af447"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9325), "https://example.com/images/asus-rog-slide1.jpg" },
                    { new Guid("734f3cd6-548b-41aa-9f2a-44939a008de0"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9404), new Guid("826f3115-6208-4188-b05e-c23e0f095cc4"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9405), "https://example.com/images/xbox-series-s-content.jpg" },
                    { new Guid("7fc3d2ce-2eaa-4b89-a611-7cb8482ff05e"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9348), new Guid("05553658-96ea-4fbe-bcdd-9bcb60714da6"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9348), "https://example.com/images/airpods-pro-slide1.jpg" },
                    { new Guid("86d36325-712f-41fd-ab1f-518857922602"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9260), new Guid("495a42be-85fd-452c-abfc-55edead730af"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9261), "https://example.com/images/galaxy-s21-content.jpg" },
                    { new Guid("8b3c5253-2360-44ed-9fb3-1769d82f480f"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9332), new Guid("2807a392-5d67-4bf8-ad60-619cbd15b389"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9332), "https://example.com/images/acer-predator-slide1.jpg" },
                    { new Guid("8ce31a07-79e3-4768-9e49-ddc3df28d149"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9276), new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9276), "https://example.com/images/galaxy-watch-slide1.jpg" },
                    { new Guid("967831b7-b251-4704-a884-74e56a1039e8"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9387), new Guid("5f587109-15d8-4b5a-af8a-d18c50cf1073"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9388), "https://example.com/images/razer-blade15-slide1.jpg" },
                    { new Guid("9faf7ae4-ff2e-4bc7-ad08-4a5f3161c354"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9400), new Guid("826f3115-6208-4188-b05e-c23e0f095cc4"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9401), "https://example.com/images/xbox-series-s-slide1.jpg" },
                    { new Guid("a1be6f15-392e-40c2-9f21-ab4d973d7232"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9397), new Guid("e8248af7-dded-446e-bcd4-de817024becf"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9398), "https://example.com/images/apple-tv4k-content.jpg" },
                    { new Guid("a6efd2e3-48e0-4c37-9e95-944f0bafbe8a"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9208), new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9209), "https://example.com/images/iphone13-content.jpg" },
                    { new Guid("a9b3ba8d-5e70-40ee-a230-aab7d2f4e8ac"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9272), new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9272), "https://example.com/images/ipad-pro-content.jpg" },
                    { new Guid("aa3c9dd1-f18a-4332-a52f-1d5a144488aa"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9338), new Guid("787ec87d-d82d-4bb9-ba3c-517e7e60d16f"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9339), "https://example.com/images/huawei-mate40-slide1.jpg" },
                    { new Guid("aa9c05dc-7ee0-44d0-8a98-c1acd1270d23"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9362), new Guid("7364d009-fb9d-4ac8-904f-fb8839dd6628"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9362), "https://example.com/images/sony-wh1000xm4-slide1.jpg" },
                    { new Guid("b5cc2da6-bbe6-4bd6-a84a-c44edbecaf61"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9391), new Guid("5f587109-15d8-4b5a-af8a-d18c50cf1073"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9391), "https://example.com/images/razer-blade15-content.jpg" },
                    { new Guid("bcf3ddac-480b-41f2-8388-c3a077b38f1a"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9351), new Guid("05553658-96ea-4fbe-bcdd-9bcb60714da6"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9352), "https://example.com/images/airpods-pro-content.jpg" },
                    { new Guid("c846d653-0014-4381-b4d1-f80d8e88945a"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9365), new Guid("7364d009-fb9d-4ac8-904f-fb8839dd6628"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9365), "https://example.com/images/sony-wh1000xm4-content.jpg" },
                    { new Guid("d5ae3de8-74fb-4c84-b5cd-fdf3f3953666"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9377), new Guid("1c2fb759-8ee2-40eb-942b-15cfc5ffe140"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9378), "https://example.com/images/hp-envy15-slide1.jpg" },
                    { new Guid("d8f6f1d0-4572-40be-b668-56765ae09a75"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9308), new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9309), "https://example.com/images/lenovo-legion5-slide1.jpg" },
                    { new Guid("d9dd3797-4370-4677-8ca4-1765288aca31"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9368), new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9369), "https://example.com/images/alienware-slide1.jpg" },
                    { new Guid("e01b75a2-fc03-46ef-9540-ad3c72f977b5"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9285), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9286), "https://example.com/images/sony-bravia-slide1.jpg" },
                    { new Guid("e53eab1b-3221-44e3-9889-abee6be5310e"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9279), new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9280), "https://example.com/images/galaxy-watch-content.jpg" },
                    { new Guid("e839b60e-a18a-47ef-801f-877b33960a20"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9293), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9294), "https://example.com/images/dell-xps13-slide1.jpg" },
                    { new Guid("f37d3ef3-411b-4f6b-8cff-29dc48e86626"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9268), new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9268), "https://example.com/images/ipad-pro-slide1.jpg" },
                    { new Guid("faa92ebd-2aef-4fc0-a23f-c66062c8524e"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9328), new Guid("76a1caa8-5d92-4dfa-83e2-09beed5af447"), null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9328), "https://example.com/images/asus-rog-content.jpg" },
                    { new Guid("fac94261-a3dd-4e59-b99b-c796b2a4a50f"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9317), new Guid("7466bf1b-6888-4d9c-a83b-697a76f82ee3"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9318), "https://example.com/images/xbox-series-x-slide1.jpg" },
                    { new Guid("fb1de928-d799-400b-8808-5736e1f43217"), null, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9212), new Guid("495a42be-85fd-452c-abfc-55edead730af"), null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9212), "https://example.com/images/galaxy-s21-slide1.jpg" }
                });

            migrationBuilder.InsertData(
                table: "invoices",
                columns: new[] { "Id", "Address", "CollaboratorId", "CreatedAt", "CustomerId", "MethodPaymment", "Name", "Note", "Status", "Telephone", "Total", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1128508f-956a-4d0d-bd29-563ced37221e"), "San Francisco, USA", new Guid("28e0105a-3dbe-45c1-8bdf-332755f6e704"), new DateTime(2024, 9, 16, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9607), new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), "Bank Transfer", "John Doe", null, 3, "123456789", null, null },
                    { new Guid("31414b0c-3bbe-4554-8486-0ac395122e64"), "New York, USA", new Guid("28e0105a-3dbe-45c1-8bdf-332755f6e704"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9590), new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), "Credit Card", "John Doe", null, 1, "123456789", null, null },
                    { new Guid("e93998d4-4f87-4e31-905d-78d701c39974"), "Los Angeles, USA", new Guid("28e0105a-3dbe-45c1-8bdf-332755f6e704"), new DateTime(2024, 9, 21, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9597), new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), "PayPal", "Jane Doe", null, 2, "987654321", null, null }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentText", "CreatedAt", "ProductId", "Rate", "UpdatedAt", "UserId", "UserReplyId" },
                values: new object[,]
                {
                    { new Guid("1a3d947f-aa4c-460a-add7-7e99bf048514"), "I thought it was okay.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(55), new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), 0, null, "e1c5db55-de77-41c1-ab45-c022d32a2034", new Guid("3fac9b73-305e-43db-91c1-bc98df10e5e4") },
                    { new Guid("34c80a15-066b-466f-bf3b-3bb932c20601"), "I had a similar issue.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(34), new Guid("495a42be-85fd-452c-abfc-55edead730af"), 0, null, "dfda172b-27f8-44d5-aef4-d029719eeab7", new Guid("68d210b6-988f-4841-8f77-7e624d87d439") },
                    { new Guid("558cf74c-dd8f-4710-9176-db6a8155fcef"), "I had a different experience with the service.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(81), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), 0, null, "dfda172b-27f8-44d5-aef4-d029719eeab7", new Guid("602d79c7-6d77-4c8e-9fb1-64d4b128bbb7") },
                    { new Guid("6b8aa4ae-d356-4ac3-afe3-fab48737b6cb"), "It depends on what you're looking for.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(69), new Guid("3fd418e6-652b-45f2-9687-224b0655e437"), 0, null, "bdf2f396-543a-4d69-ad81-e346218aec1e", new Guid("3fac9b73-305e-43db-91c1-bc98df10e5e4") },
                    { new Guid("7c3c5eab-c53d-426d-9a31-77d47e23110e"), "I agree with you!", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9988), new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), 0, null, "e1c5db55-de77-41c1-ab45-c022d32a2034", new Guid("a64613fe-8ff4-4339-8964-5ef3e36e876c") },
                    { new Guid("7ca167e4-43f1-4f2d-bfdf-7cb4a725e494"), "They were very helpful for me too.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(91), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf"), 0, null, "dfd365b0-ed6c-4627-8d91-6f6488d301b1", new Guid("602d79c7-6d77-4c8e-9fb1-64d4b128bbb7") },
                    { new Guid("7d79e633-1e45-4102-a81b-f5c6b63e82e0"), "Why do you think so?", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(22), new Guid("495a42be-85fd-452c-abfc-55edead730af"), 0, null, "bdf2f396-543a-4d69-ad81-e346218aec1e", new Guid("68d210b6-988f-4841-8f77-7e624d87d439") },
                    { new Guid("a27ff99b-64fd-4e94-810c-658ddce8a56f"), "Did you contact support?", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(102), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), 0, null, "bdf2f396-543a-4d69-ad81-e346218aec1e", new Guid("79ec51b2-f376-4be3-8f18-5fa0e56f6662") },
                    { new Guid("a2f28ba7-6384-4ba3-ac3c-d42bdba79247"), "I agree, it's a great product.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(46), new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a"), 0, null, "dfd365b0-ed6c-4627-8d91-6f6488d301b1", new Guid("fd08bbf6-1d51-41e6-a2fd-1ba7fe1d4807") },
                    { new Guid("b2dc47f0-c0c5-439e-a8ff-31508501b6a6"), "I had a different experience.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(14), new Guid("99144d35-20d1-45bb-9301-d7275b874d94"), 0, null, "dfda172b-27f8-44d5-aef4-d029719eeab7", new Guid("a64613fe-8ff4-4339-8964-5ef3e36e876c") },
                    { new Guid("dc03242a-20e3-407f-b46a-d94941199f1f"), "I had the same issue. They sent a replacement quickly.", new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(126), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628"), 0, null, "dfd365b0-ed6c-4627-8d91-6f6488d301b1", new Guid("79ec51b2-f376-4be3-8f18-5fa0e56f6662") }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "CreateAt", "IsCustomer", "Text_message" },
                values: new object[,]
                {
                    { new Guid("0d4ac38f-86b1-4ad2-8759-2e11a31cca6c"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 43, 52, 979, DateTimeKind.Local).AddTicks(205), true, "Im fine, but my boss at FPT 'Chen ep' me, i can't work at the company anymore" },
                    { new Guid("0d4ad93e-e3cd-4b07-adbc-671d1a6f3b8f"), new Guid("70d16e23-5df5-486e-8e68-e49aa8550a9c"), new DateTime(2024, 9, 26, 9, 48, 52, 979, DateTimeKind.Local).AddTicks(221), false, "F*ck you" },
                    { new Guid("196b793b-bb3e-46ba-b35f-690eafb0023f"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 42, 52, 979, DateTimeKind.Local).AddTicks(201), false, "Hello, how are you today" },
                    { new Guid("1a224c1a-c782-4b28-a6cc-4ddc681a22e7"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 46, 52, 979, DateTimeKind.Local).AddTicks(213), false, "Im sorry about that, i hear so many the complain about your boss" },
                    { new Guid("567409c2-2100-4d78-96a1-dd02a55e342c"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 44, 52, 979, DateTimeKind.Local).AddTicks(207), false, "Your boss is Nguyen Phuc Du" },
                    { new Guid("7f9a27f1-55f3-4d07-a5ab-9efae69cb2eb"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 45, 52, 979, DateTimeKind.Local).AddTicks(210), true, "Yes, i working like a dog day and day, and he not allow me rest, alse the holiday he assign so many task to me" },
                    { new Guid("c32757c7-8cbc-4119-90d7-65171d69bf4d"), new Guid("70d16e23-5df5-486e-8e68-e49aa8550a9c"), new DateTime(2024, 9, 26, 9, 47, 52, 979, DateTimeKind.Local).AddTicks(216), true, "Hello" },
                    { new Guid("da8ec773-6b4a-445d-9f4b-4242a5ccead9"), new Guid("61440a81-0cbf-4ba4-a9d8-be04f00b9109"), new DateTime(2024, 9, 26, 9, 41, 52, 979, DateTimeKind.Local).AddTicks(196), true, "Hello you" }
                });

            migrationBuilder.InsertData(
                table: "ProductColors",
                columns: new[] { "Id", "CreatedAt", "Price", "ProductHardwareId", "Quantity", "RGB", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("042e6999-2285-4788-8f45-18488585cddc"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9521), 1199.99, new Guid("4bc53e12-65a6-4699-a5ce-db60c5a29319"), 30, "#C0C0C0", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9522) },
                    { new Guid("0a22d2f7-ae0e-4986-9642-8dbcd8ecf98d"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9458), 999.99000000000001, new Guid("8a8baa65-294c-4e58-89e2-cf81359c2d93"), 50, "#F8F8F8", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9458) },
                    { new Guid("120bb387-2412-47f3-9860-cf65f98022d3"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9441), 849.99000000000001, new Guid("dc71e128-e94c-4f43-9739-bee2b9baf820"), 70, "#6D6E71", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9442) },
                    { new Guid("1a70e5ae-71f8-42ca-9518-b5e6998ce8b3"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9582), 1199.99, new Guid("b0018fde-172d-412e-bfd4-e7497e18f848"), 8, "#7B7B7B", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9583) },
                    { new Guid("1aa6ef6f-fd2d-44ba-b377-a1e218303b5e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9542), 1299.99, new Guid("13d20162-6371-43bd-b866-21224b0fb437"), 30, "#CCCCCC", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9543) },
                    { new Guid("1e414639-10d2-4acc-a8ff-f6899551cdbf"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9428), 999.99000000000001, new Guid("d9035c57-39a4-4c94-b35a-469899e39d62"), 30, "#FF0000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9428) },
                    { new Guid("2d54b5a0-6fa5-4432-90b8-7217fc8f22e5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9455), 999.99000000000001, new Guid("8a8baa65-294c-4e58-89e2-cf81359c2d93"), 55, "#6D6E71", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9455) },
                    { new Guid("3de9cdf9-89cb-4772-8b66-107186ad54cb"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9512), 999.99000000000001, new Guid("83207d05-25da-476b-b957-1df38ce55840"), 75, "#3C3C3C", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9513) },
                    { new Guid("45f8192b-00e6-4f28-8e5e-b86fd0298cba"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9560), 1199.99, new Guid("f3e62faf-2956-4667-9501-2e94435e0c44"), 18, "#4B4B4B", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9560) },
                    { new Guid("52a3b2df-d5cb-4bd5-8bb9-f70a816b14c5"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9539), 1299.99, new Guid("13d20162-6371-43bd-b866-21224b0fb437"), 35, "#FFFFFF", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9540) },
                    { new Guid("55341dcd-b668-4f86-9f20-fa976e086e7d"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9424), 999.99000000000001, new Guid("d9035c57-39a4-4c94-b35a-469899e39d62"), 40, "#FFFFFF", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9425) },
                    { new Guid("56ded32a-e2a3-4fac-b78a-91355df7f609"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9464), 799.99000000000001, new Guid("a241ff16-4d75-4880-b0ee-5807284ecd45"), 85, "#3C3C3C", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9465) },
                    { new Guid("5dfc0fc2-0f59-4bf6-a14c-74233436ad60"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9557), 1199.99, new Guid("f3e62faf-2956-4667-9501-2e94435e0c44"), 20, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9557) },
                    { new Guid("5e886cf9-7111-4f2a-a77a-916d0c3038e7"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9570), 1499.99, new Guid("b9528a02-76f2-4314-9f78-bba0ea096e51"), 15, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9571) },
                    { new Guid("7375e781-94e4-42f1-a1a2-2161e433f8eb"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9461), 999.99000000000001, new Guid("8a8baa65-294c-4e58-89e2-cf81359c2d93"), 45, "#8C4B9A", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9461) },
                    { new Guid("87fc82e9-9e05-4f88-9a49-13c590c22534"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9438), 799.99000000000001, new Guid("fc23ca85-6bf4-4821-b1ca-c19478e12f17"), 45, "#FF0000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9439) },
                    { new Guid("911a4a78-9a4e-4c61-bd62-5b0bb94d53d4"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9551), 499.99000000000001, new Guid("4be38d4a-28fc-4a20-a4dd-af0e3bff924a"), 45, "#FFFFFF", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9551) },
                    { new Guid("9dd2f04b-de01-4295-9cab-6e6bf6992fbe"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9435), 799.99000000000001, new Guid("fc23ca85-6bf4-4821-b1ca-c19478e12f17"), 50, "#FFFFFF", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9435) },
                    { new Guid("a7183b5b-3fb8-4319-a450-2ef9846f0800"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9418), 999.99000000000001, new Guid("d9035c57-39a4-4c94-b35a-469899e39d62"), 50, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9418) },
                    { new Guid("ae1d719f-4cac-4e30-becd-328606470450"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9528), 1399.99, new Guid("2dd54462-a38b-4435-a06b-93faa9b4c2ec"), 35, "#C0C0C0", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9529) },
                    { new Guid("b433895b-4933-4b08-b851-5d79bbcf634d"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9567), 499.99000000000001, new Guid("efb9d89c-d4c1-4bd7-b82d-ceb9d57edc6a"), 20, "#4F4F4F", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9568) },
                    { new Guid("b5dbe8fb-ef82-421a-856e-8dff2bd284f2"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9578), 1199.99, new Guid("b0018fde-172d-412e-bfd4-e7497e18f848"), 10, "#FFFFFF", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9579) },
                    { new Guid("bb4aac6b-2019-493e-98f7-abcf22369dd9"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9545), 499.99000000000001, new Guid("4be38d4a-28fc-4a20-a4dd-af0e3bff924a"), 50, "#8B8B8B", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9546) },
                    { new Guid("c0257381-b7ac-4ead-b4e6-30fe4cd6fccd"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9516), 999.99000000000001, new Guid("83207d05-25da-476b-b957-1df38ce55840"), 70, "#C0C0C0", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9516) },
                    { new Guid("c4683db0-7530-40b1-888b-f9cfc22b08f6"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9509), 799.99000000000001, new Guid("a241ff16-4d75-4880-b0ee-5807284ecd45"), 80, "#C0C0C0", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9509) },
                    { new Guid("d000dcb8-9861-436f-ac06-ecb29a0a9f63"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9554), 499.99000000000001, new Guid("4be38d4a-28fc-4a20-a4dd-af0e3bff924a"), 40, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9554) },
                    { new Guid("d766c004-7f88-46de-95bf-32dbfb273d02"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9431), 799.99000000000001, new Guid("fc23ca85-6bf4-4821-b1ca-c19478e12f17"), 60, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9432) },
                    { new Guid("ddcd96bf-f396-439e-8e4d-081892533308"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9525), 1199.99, new Guid("4bc53e12-65a6-4699-a5ce-db60c5a29319"), 25, "#F7F7F7", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9525) },
                    { new Guid("dfaa0de7-9e21-4d7d-aae9-6d4886aa4c3e"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9573), 1499.99, new Guid("b9528a02-76f2-4314-9f78-bba0ea096e51"), 12, "#343434", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9574) },
                    { new Guid("e0117e3d-4523-4a2e-90be-4133b068a255"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9534), 1299.99, new Guid("13d20162-6371-43bd-b866-21224b0fb437"), 40, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9536) },
                    { new Guid("e13e346b-cb82-4574-a5a9-605e7fdcead6"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9444), 849.99000000000001, new Guid("dc71e128-e94c-4f43-9739-bee2b9baf820"), 65, "#F8F8F8", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9445) },
                    { new Guid("e45d640a-72e3-45e2-baa4-c23a2b89bee3"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9451), 849.99000000000001, new Guid("dc71e128-e94c-4f43-9739-bee2b9baf820"), 60, "#8C4B9A", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9452) },
                    { new Guid("ec6dbc2d-5fe1-4138-8fa4-659668ded781"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9531), 1399.99, new Guid("2dd54462-a38b-4435-a06b-93faa9b4c2ec"), 30, "#F7F7F7", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9532) },
                    { new Guid("efdc63b3-2199-431a-86ab-1edac98d6181"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9563), 499.99000000000001, new Guid("efb9d89c-d4c1-4bd7-b82d-ceb9d57edc6a"), 25, "#000000", new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9563) }
                });

            migrationBuilder.InsertData(
                table: "ProductFilters",
                columns: new[] { "FilterId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("37d35afd-167d-4be2-915d-80074b52c4b8"), new Guid("7466bf1b-6888-4d9c-a83b-697a76f82ee3") },
                    { new Guid("37d35afd-167d-4be2-915d-80074b52c4b8"), new Guid("e8248af7-dded-446e-bcd4-de817024becf") },
                    { new Guid("3c4265fb-955d-4e2d-9d7f-2f40ca0164ff"), new Guid("1c2fb759-8ee2-40eb-942b-15cfc5ffe140") },
                    { new Guid("3c4265fb-955d-4e2d-9d7f-2f40ca0164ff"), new Guid("d8941bfd-43dc-40d0-a03a-11798de391fc") },
                    { new Guid("54794ede-52fb-46fb-b35f-9285d51338eb"), new Guid("7364d009-fb9d-4ac8-904f-fb8839dd6628") },
                    { new Guid("54794ede-52fb-46fb-b35f-9285d51338eb"), new Guid("e3589f3d-78ca-4a76-9624-caf516166ddf") },
                    { new Guid("5a6e4e4e-f8ce-46f7-b5e2-7e8968ce43ed"), new Guid("5f587109-15d8-4b5a-af8a-d18c50cf1073") },
                    { new Guid("5a6e4e4e-f8ce-46f7-b5e2-7e8968ce43ed"), new Guid("a8840f4b-3d25-4737-89b1-5c359c05cb00") },
                    { new Guid("67507f8c-2351-48b7-b07e-6b50b69fb7db"), new Guid("2807a392-5d67-4bf8-ad60-619cbd15b389") },
                    { new Guid("67507f8c-2351-48b7-b07e-6b50b69fb7db"), new Guid("99144d35-20d1-45bb-9301-d7275b874d94") },
                    { new Guid("894f8507-cf37-4d63-9c16-993b543654a6"), new Guid("495a42be-85fd-452c-abfc-55edead730af") },
                    { new Guid("894f8507-cf37-4d63-9c16-993b543654a6"), new Guid("787ec87d-d82d-4bb9-ba3c-517e7e60d16f") },
                    { new Guid("90abdddb-d6a9-414f-b40d-70e1756a0b83"), new Guid("05553658-96ea-4fbe-bcdd-9bcb60714da6") },
                    { new Guid("90abdddb-d6a9-414f-b40d-70e1756a0b83"), new Guid("c91d55b2-fae9-4da6-b248-c1f6be6e439a") },
                    { new Guid("a94775da-e452-4270-9a05-537f103cd70b"), new Guid("755696b8-c634-4bcd-8382-fed5b9db3e38") },
                    { new Guid("a94775da-e452-4270-9a05-537f103cd70b"), new Guid("96f0fead-2b10-4c3e-b5d4-5d788dfc7628") },
                    { new Guid("fa45c0be-014d-4e41-bbd4-4fc7bec97358"), new Guid("3fd418e6-652b-45f2-9687-224b0655e437") },
                    { new Guid("fa45c0be-014d-4e41-bbd4-4fc7bec97358"), new Guid("c3686570-7258-447e-8495-f776f2298ee0") }
                });

            migrationBuilder.InsertData(
                table: "InvoiceProducts",
                columns: new[] { "InvoiceId", "ProductColorsId", "CreatedAt", "ProductPrice", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1128508f-956a-4d0d-bd29-563ced37221e"), new Guid("87fc82e9-9e05-4f88-9a49-13c590c22534"), null, 1199.99, 1, null },
                    { new Guid("1128508f-956a-4d0d-bd29-563ced37221e"), new Guid("9dd2f04b-de01-4295-9cab-6e6bf6992fbe"), null, 1199.99, 1, null },
                    { new Guid("31414b0c-3bbe-4554-8486-0ac395122e64"), new Guid("55341dcd-b668-4f86-9f20-fa976e086e7d"), null, 799.99000000000001, 2, null },
                    { new Guid("31414b0c-3bbe-4554-8486-0ac395122e64"), new Guid("a7183b5b-3fb8-4319-a450-2ef9846f0800"), null, 999.99000000000001, 1, null },
                    { new Guid("e93998d4-4f87-4e31-905d-78d701c39974"), new Guid("1e414639-10d2-4acc-a8ff-f6899551cdbf"), null, 849.99000000000001, 1, null },
                    { new Guid("e93998d4-4f87-4e31-905d-78d701c39974"), new Guid("d766c004-7f88-46de-95bf-32dbfb273d02"), null, 999.99000000000001, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "ProductColorId", "CreatedAt", "OrderCustomerId", "OrderProductColorId", "Quanitity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), new Guid("1e414639-10d2-4acc-a8ff-f6899551cdbf"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9773), null, null, 3, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9774) },
                    { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), new Guid("55341dcd-b668-4f86-9f20-fa976e086e7d"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9771), null, null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9771) },
                    { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), new Guid("9dd2f04b-de01-4295-9cab-6e6bf6992fbe"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9778), null, null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9779) },
                    { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), new Guid("a7183b5b-3fb8-4319-a450-2ef9846f0800"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9767), null, null, 2, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9768) },
                    { new Guid("0f2456ef-da81-4455-9dfc-e5d6d4205849"), new Guid("d766c004-7f88-46de-95bf-32dbfb273d02"), new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9776), null, null, 1, new DateTime(2024, 9, 26, 9, 41, 52, 978, DateTimeKind.Local).AddTicks(9776) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserID",
                table: "Admins",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CollaboratorId",
                table: "Chats",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CustomerId",
                table: "Chats",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_InvoiceId",
                table: "Collaborators",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserID",
                table: "Collaborators",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserReplyId",
                table: "Comments",
                column: "UserReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserID",
                table: "Customers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_CategoryId",
                table: "Filters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_SpecificationId",
                table: "Filters",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProducts_ProductColorsId",
                table: "InvoiceProducts",
                column: "ProductColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_CustomerId",
                table: "invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCustomerId_OrderProductColorId",
                table: "Orders",
                columns: new[] { "OrderCustomerId", "OrderProductColorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductColorId",
                table: "Orders",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductHardwareId",
                table: "ProductColors",
                column: "ProductHardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_ProductId",
                table: "ProductFilters",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHardwares_ProductId",
                table: "ProductHardwares",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_CategoryId",
                table: "Specifications",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "InvoiceProducts");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductFilters");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "ProductHardwares");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
