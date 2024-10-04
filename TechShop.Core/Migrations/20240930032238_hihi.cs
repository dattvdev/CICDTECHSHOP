using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechShop.Core.Migrations
{
    /// <inheritdoc />
    public partial class hihi : Migration
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
                    { "03f424ef-a625-489a-9a51-bac8dc88df4a", null, "Admin", "ADMIN" },
                    { "605cce82-4026-467d-8620-97a7db6f6273", null, "Collaborator", "COLLABORATOR" },
                    { "8758368f-fce5-46ad-8ac4-f864c47114bd", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDay", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "415a79a3-ea4f-4bb7-a6dc-9abf82cba517", 0, "456 Elm St, Los Angeles, CA", new DateTime(2003, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "f9a1789d-f276-4ade-bcda-845c88d4715e", new DateTime(2024, 9, 30, 10, 22, 35, 944, DateTimeKind.Local).AddTicks(9915), "khanhtmq2@fpt.com", false, "Tran Minh Quoc Khanh", false, null, null, null, "AQAAAAIAAYagAAAAELpW/ud/bTvuJ+IynsxGqDRV8U88bnGcW/LRW/bYY7AVcb6m96KpLETs83TujMWw6g==", "9876543210", false, "2d47629a-aee2-4db4-b2ac-91f6edb9d26f", false, new DateTime(2024, 9, 30, 10, 22, 35, 944, DateTimeKind.Local).AddTicks(9923), "khanhtmq2" },
                    { "7b22ab2c-9296-459e-84f2-2adb73ae5eb2", 0, "707 Cherry St, Phoenix, AZ", new DateTime(2002, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "287c263a-1683-4006-9ff9-3c1ec7e1dd5b", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(6578), "anhbhn@fpt.com", false, "Bui Huynh Ngoc Anh", false, null, null, null, "AQAAAAIAAYagAAAAELvw/uPjxPxY8xPH5YGKtvh1iL8Nn5vcTrBVlDm9c+AIKU8C80nHmD83KhXYmlH2vw==", "8886785432", false, "b9061f9f-cd2a-4120-a428-903a00ca0519", false, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(6584), "anhbhn" },
                    { "821ea039-ebbf-4105-a966-065738232bbf", 0, "789 Pine St, Chicago, IL", new DateTime(2003, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "afa3c55c-780f-4c9b-a075-513e0028a934", new DateTime(2024, 9, 30, 10, 22, 36, 30, DateTimeKind.Local).AddTicks(1447), "dattv20@fpt.com", false, "Tran Van Dat", false, null, null, null, "AQAAAAIAAYagAAAAEIkAxMqT3jsU+w4XFw8iXakAu1ziiSyoQxNe3jZNB0fYgPMXLU5rfqE5TApJyRH+HQ==", "5551234567", false, "e424eba0-a5b6-4642-adab-118afded2667", false, new DateTime(2024, 9, 30, 10, 22, 36, 30, DateTimeKind.Local).AddTicks(1460), "dattv20" },
                    { "8d264581-742d-41be-bd07-78c0453e875e", 0, "123 Main St, New York, NY", new DateTime(2001, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "deee555d-14c8-4f1c-8ece-c23677d14a01", new DateTime(2024, 9, 30, 10, 22, 35, 841, DateTimeKind.Local).AddTicks(9327), "phucnd20@fpt.com", false, "Nguyen Duy Phuc", false, null, null, null, "AQAAAAIAAYagAAAAEHXEtLomoMvLo3sMPnHeTjhq8nVK0QTHnSCTTgFc45ltR3d6Y2e3F7I1F6AxqZDtZg==", "1234567890", false, "6971ee70-c565-423c-9a05-852d81c321ab", false, new DateTime(2024, 9, 30, 10, 22, 35, 841, DateTimeKind.Local).AddTicks(9336), "phucnd20" },
                    { "8fd53c73-9883-4d1c-b084-3e35853e15cf", 0, "202 Oak St, Miami, FL", new DateTime(2003, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "7bd56a2e-7ef2-48c1-ac12-a7e6f3e68e03", new DateTime(2024, 9, 30, 10, 22, 36, 206, DateTimeKind.Local).AddTicks(6847), "duclt24@fpt.com", false, "Le Toan Duc", false, null, null, null, "AQAAAAIAAYagAAAAEDveWm/XsT0AQUdAo5x38y4w+o4hgFQwffIbCDTR13ZE+vvdHhqjB3/0S4wb0DFarg==", "3335678901", false, "455186ea-22df-4c12-b8f5-c94302a35fff", false, new DateTime(2024, 9, 30, 10, 22, 36, 206, DateTimeKind.Local).AddTicks(6853), "duclt24" },
                    { "c28a969e-7d9d-4375-935c-94fd7a32447b", 0, "101 Maple St, San Francisco, CA", new DateTime(2002, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "640c2e70-7224-4958-b1c0-c196fd0783cb", new DateTime(2024, 9, 30, 10, 22, 36, 106, DateTimeKind.Local).AddTicks(9737), "nhannt96@fpt.com", false, "Nguyen Tri Nhan", false, null, null, null, "AQAAAAIAAYagAAAAEGFD+GTsyVC7F+feME5Ks7qwfKieHgpdKgbaO/ZjFULbCP9tje8+8s8VkZL5dd/sOw==", "4449876543", false, "615fd266-4e47-4a84-9763-aa56547aa482", false, new DateTime(2024, 9, 30, 10, 22, 36, 106, DateTimeKind.Local).AddTicks(9743), "nhannt96" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("3756f15d-dfb0-4f59-8970-cbfba08d6a0c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7683), null, "Lenovo", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7684), "lenovo" },
                    { new Guid("4378e25a-b9af-4cfb-aab6-50d39d4acc7a"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7674), null, "Dell", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7674), "dell" },
                    { new Guid("47d6c021-87a6-44c0-bf6d-d400ce8aeef2"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7643), null, "Apple", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7645), "apple" },
                    { new Guid("69d1badd-25a1-4d32-8b55-fef6f7b519ed"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7656), null, "Samsung", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7657), "samsung" },
                    { new Guid("8289000d-d3c4-4f82-9a2f-8f8b2c969b22"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7678), null, "HP", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7679), "hp" },
                    { new Guid("9b245f1d-e6fa-4419-a832-a059f4b67fef"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7692), null, "Asus", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7692), "asus" },
                    { new Guid("d9bd6258-c767-4c70-8451-8327ec3b9e87"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7696), null, "Acer", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7696), "acer" },
                    { new Guid("e43f1172-920a-465f-a2ae-6f249615127d"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7664), null, "Sony", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7665), "sony" },
                    { new Guid("e8ef4371-53f4-4a5e-a466-02314813db37"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7688), null, "Microsoft", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7689), "microsoft" },
                    { new Guid("e964ac17-9719-402c-b0a2-01951d1b31d5"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7700), null, "Huawei", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7701), "huawei" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("08316261-0d65-42da-a033-0320c202bbfa"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7580), null, "Watches", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7581), "watches" },
                    { new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7533), null, "Phones", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7534), "phones" },
                    { new Guid("3ed05c58-1153-40d5-8bea-4f37757eaee4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7602), null, "TVs", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7603), "tvs" },
                    { new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7546), null, "Laptops", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7547), "laptops" },
                    { new Guid("9a32ac7e-b354-44a9-b9e2-ef6a2458d76f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7619), null, "Cameras", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7620), "cameras" },
                    { new Guid("9fec396e-94c7-426b-a79a-f9345ea94de4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7563), null, "Tablets", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7564), "tablets" },
                    { new Guid("b75fdf86-928a-4311-897d-dfdc98ffbcdf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7585), null, "PCs", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7592), "pcs" },
                    { new Guid("dd515d07-2b68-429c-af48-5910917bbadf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7610), null, "Accessories", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7611), "accessories" },
                    { new Guid("e863a608-ba3d-4c19-88aa-ccf8fbb887cd"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7614), null, "Audio", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7615), "audio" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "AccessLevel", "UserID" },
                values: new object[] { new Guid("af13f1a2-bd0f-40a2-980a-65ae20860c91"), 2, "8d264581-742d-41be-bd07-78c0453e875e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "605cce82-4026-467d-8620-97a7db6f6273", "415a79a3-ea4f-4bb7-a6dc-9abf82cba517" },
                    { "8758368f-fce5-46ad-8ac4-f864c47114bd", "7b22ab2c-9296-459e-84f2-2adb73ae5eb2" },
                    { "605cce82-4026-467d-8620-97a7db6f6273", "821ea039-ebbf-4105-a966-065738232bbf" },
                    { "03f424ef-a625-489a-9a51-bac8dc88df4a", "8d264581-742d-41be-bd07-78c0453e875e" },
                    { "8758368f-fce5-46ad-8ac4-f864c47114bd", "8fd53c73-9883-4d1c-b084-3e35853e15cf" },
                    { "605cce82-4026-467d-8620-97a7db6f6273", "c28a969e-7d9d-4375-935c-94fd7a32447b" }
                });

            migrationBuilder.InsertData(
                table: "Collaborators",
                columns: new[] { "ID", "EndDate", "InvoiceId", "StartDate", "UserID" },
                values: new object[,]
                {
                    { new Guid("09bf0abb-f2de-4269-9bd9-89b655574c1c"), new DateTime(2025, 2, 28, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7451), null, new DateTime(2024, 6, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7449), "821ea039-ebbf-4105-a966-065738232bbf" },
                    { new Guid("b501f3bf-e448-48ba-b5e3-2f04f5edafaf"), new DateTime(2025, 2, 28, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7463), null, new DateTime(2024, 7, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7462), "8fd53c73-9883-4d1c-b084-3e35853e15cf" },
                    { new Guid("d7bdf57d-2249-4de5-be3f-3509ee5e6e7d"), new DateTime(2025, 2, 28, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7419), null, new DateTime(2024, 4, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7410), "415a79a3-ea4f-4bb7-a6dc-9abf82cba517" },
                    { new Guid("e4e6f358-cd42-43e0-a6b5-a57ffd4f7d85"), new DateTime(2025, 2, 28, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7458), null, new DateTime(2024, 7, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7456), "c28a969e-7d9d-4375-935c-94fd7a32447b" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "CreatedAt", "LoyaltyPoints", "UpdatedAt", "UserID" },
                values: new object[] { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), null, 150, null, "7b22ab2c-9296-459e-84f2-2adb73ae5eb2" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "Description", "Discount", "Image", "Name", "UpdatedAt", "UrlSlug" },
                values: new object[,]
                {
                    { new Guid("03727abf-ffb5-4a6a-85e5-a65e6b7e8b77"), new Guid("47d6c021-87a6-44c0-bf6d-d400ce8aeef2"), new Guid("dd515d07-2b68-429c-af48-5910917bbadf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7925), "Wireless earbuds with active noise cancellation.", 5, "https://example.com/images/airpods-pro.jpg", "Apple AirPods Pro", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7926), "airpods-pro" },
                    { new Guid("11f1f291-763c-49ad-b4d2-2372dbaa6210"), new Guid("9b245f1d-e6fa-4419-a832-a059f4b67fef"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7812), "Ultra-thin gaming laptop with AMD Ryzen 9 processor.", 15, "https://example.com/images/asus-rog.jpg", "Asus ROG Zephyrus", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7813), "asus-rog-zephyrus" },
                    { new Guid("159ff9d4-b243-4681-a22e-39e1702f5bd0"), new Guid("e8ef4371-53f4-4a5e-a466-02314813db37"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7973), "Dual-screen device for productivity on the go.", 10, "https://example.com/images/surface-duo.jpg", "Microsoft Surface Duo", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7974), "microsoft-surface-duo" },
                    { new Guid("20d019d7-56f5-481e-8419-2fe708b8f50b"), new Guid("d9bd6258-c767-4c70-8451-8327ec3b9e87"), new Guid("e863a608-ba3d-4c19-88aa-ccf8fbb887cd"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7985), "Convertible laptop with a sleek design.", 20, "https://example.com/images/acer-spin5.jpg", "Acer Spin 5", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7986), "acer-spin5" },
                    { new Guid("60072ca9-8a74-4244-b219-973e8c65b7c8"), new Guid("9b245f1d-e6fa-4419-a832-a059f4b67fef"), new Guid("08316261-0d65-42da-a033-0320c202bbfa"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7979), "Stylish smartwatch with customizable faces.", 15, "https://example.com/images/asus-zenwatch3.jpg", "Asus ZenWatch 3", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7980), "asus-zenwatch3" },
                    { new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6"), new Guid("8289000d-d3c4-4f82-9a2f-8f8b2c969b22"), new Guid("b75fdf86-928a-4311-897d-dfdc98ffbcdf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7783), "Affordable desktop with AMD Ryzen processor.", 20, "https://example.com/images/hp-pavilion.jpg", "HP Pavilion Desktop", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7784), "hp-pavilion" },
                    { new Guid("6cb3acc9-d11f-46a6-8fdb-58bc377a9f02"), new Guid("d9bd6258-c767-4c70-8451-8327ec3b9e87"), new Guid("b75fdf86-928a-4311-897d-dfdc98ffbcdf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7818), "Compact gaming desktop with powerful performance.", 20, "https://example.com/images/acer-predator.jpg", "Acer Predator Orion 3000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7819), "acer-predator-orion" },
                    { new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), new Guid("47d6c021-87a6-44c0-bf6d-d400ce8aeef2"), new Guid("9fec396e-94c7-426b-a79a-f9345ea94de4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7752), "Apple iPad Pro with M1 chip.", 7, "https://example.com/images/ipad-pro.jpg", "iPad Pro", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7753), "ipad-pro" },
                    { new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), new Guid("e43f1172-920a-465f-a2ae-6f249615127d"), new Guid("3ed05c58-1153-40d5-8bea-4f37757eaee4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7771), "4K UHD Smart TV with OLED display.", 15, "https://example.com/images/sony-bravia.jpg", "Sony Bravia 55-inch", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7772), "sony-bravia" },
                    { new Guid("7a47207f-eed6-425b-adb4-6b764de3dc59"), new Guid("8289000d-d3c4-4f82-9a2f-8f8b2c969b22"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7959), "Sleek laptop with powerful Intel i7 processor.", 12, "https://example.com/images/hp-envy15.jpg", "HP Envy 15", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7959), "hp-envy15" },
                    { new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), new Guid("4378e25a-b9af-4cfb-aab6-50d39d4acc7a"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7777), "Compact and powerful ultrabook with Intel i7 processor.", 10, "https://example.com/images/dell-xps13.jpg", "Dell XPS 13", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7778), "dell-xps13" },
                    { new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), new Guid("69d1badd-25a1-4d32-8b55-fef6f7b519ed"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7746), "Flagship Samsung phone with Exynos 2100.", 10, "https://example.com/images/galaxy-s21.jpg", "Samsung Galaxy S21", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7746), "galaxy-s21" },
                    { new Guid("8801342a-9f51-47df-827d-9d98f0962882"), new Guid("e43f1172-920a-465f-a2ae-6f249615127d"), new Guid("9a32ac7e-b354-44a9-b9e2-ef6a2458d76f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7939), "Industry-leading noise canceling headphones.", 12, "https://example.com/images/sony-wh1000xm4.jpg", "Sony WH-1000XM4", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7940), "sony-wh1000xm4" },
                    { new Guid("8e92b5d8-3a67-48cb-b11b-5b781d2e85df"), new Guid("69d1badd-25a1-4d32-8b55-fef6f7b519ed"), new Guid("e863a608-ba3d-4c19-88aa-ccf8fbb887cd"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7932), "True wireless earbuds with premium sound.", 8, "https://example.com/images/galaxy-buds.jpg", "Samsung Galaxy Buds Live", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7933), "galaxy-buds-live" },
                    { new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), new Guid("69d1badd-25a1-4d32-8b55-fef6f7b519ed"), new Guid("08316261-0d65-42da-a033-0320c202bbfa"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7759), "Smartwatch with advanced health tracking features.", 12, "https://example.com/images/galaxy-watch.jpg", "Samsung Galaxy Watch", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7760), "galaxy-watch" },
                    { new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), new Guid("47d6c021-87a6-44c0-bf6d-d400ce8aeef2"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7729), "Latest Apple iPhone with A15 Bionic chip.", 5, "https://example.com/images/iphone13.jpg", "iPhone 13", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7730), "iphone-13" },
                    { new Guid("d37bb274-8a87-4682-9acb-f07650b04918"), new Guid("e964ac17-9719-402c-b0a2-01951d1b31d5"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7824), "Flagship Huawei smartphone with cutting-edge technology.", 10, "https://example.com/images/huawei-mate40.jpg", "Huawei Mate 40 Pro", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7824), "huawei-mate40-pro" },
                    { new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4"), new Guid("3756f15d-dfb0-4f59-8970-cbfba08d6a0c"), new Guid("9a32ac7e-b354-44a9-b9e2-ef6a2458d76f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7791), "High-performance gaming laptop with NVIDIA RTX graphics.", 18, "https://example.com/images/lenovo-legion5.jpg", "Lenovo Legion 5", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7792), "lenovo-legion5" },
                    { new Guid("d6efaed7-35b4-4344-b71d-6e50cea8caf4"), new Guid("3756f15d-dfb0-4f59-8970-cbfba08d6a0c"), new Guid("9fec396e-94c7-426b-a79a-f9345ea94de4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7967), "Versatile tablet with built-in kickstand.", 7, "https://example.com/images/lenovo-yoga.jpg", "Lenovo Yoga Smart Tab", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7968), "lenovo-yoga-smart-tab" },
                    { new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0"), new Guid("4378e25a-b9af-4cfb-aab6-50d39d4acc7a"), new Guid("9a32ac7e-b354-44a9-b9e2-ef6a2458d76f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7949), "Top-tier gaming desktop with cutting-edge hardware.", 18, "https://example.com/images/alienware.jpg", "Dell Alienware", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7950), "dell-alienware" },
                    { new Guid("fbfb9d1d-ac03-442d-8d95-43e82ffb9297"), new Guid("e8ef4371-53f4-4a5e-a466-02314813db37"), new Guid("9a32ac7e-b354-44a9-b9e2-ef6a2458d76f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7804), "Next-gen gaming console with 4K gaming capabilities.", 5, "https://example.com/images/xbox-series-x.jpg", "Microsoft Xbox Series X", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(7806), "xbox-series-x" }
                });

            migrationBuilder.InsertData(
                table: "Specifications",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("14b6611e-5ff7-40d0-bea4-43f4237f58c1"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), "Storage" },
                    { new Guid("1adadffa-554d-4558-8ca6-c7fc8891e9d5"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), "RAM" },
                    { new Guid("63782ae9-4332-42e1-b945-d0293368477e"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), "Chip" },
                    { new Guid("7108c6ce-ab0c-4de8-adc1-6c2658f06838"), new Guid("3ed05c58-1153-40d5-8bea-4f37757eaee4"), "Resolution" },
                    { new Guid("8ad58586-7727-419c-8bf2-80eb908d462e"), new Guid("b75fdf86-928a-4311-897d-dfdc98ffbcdf"), "Processor" },
                    { new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), "RAM" },
                    { new Guid("a5e69a73-4866-45bd-9058-0ca84c512724"), new Guid("3ed05c58-1153-40d5-8bea-4f37757eaee4"), "Screen Size" },
                    { new Guid("eddc8d1b-ff6c-4303-a959-0ef8c48f1019"), new Guid("b75fdf86-928a-4311-897d-dfdc98ffbcdf"), "Graphic Card" }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "CollaboratorId", "CreatedAt", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("3d7bc032-2950-4d50-9502-028d1b6c8341"), new Guid("09bf0abb-f2de-4269-9bd9-89b655574c1c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9824), new Guid("7e63353d-082c-403a-b8ec-88302752b402") },
                    { new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new Guid("d7bdf57d-2249-4de5-be3f-3509ee5e6e7d"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9810), new Guid("7e63353d-082c-403a-b8ec-88302752b402") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentText", "CreatedAt", "ProductId", "Rate", "UpdatedAt", "UserId", "UserReplyId" },
                values: new object[,]
                {
                    { new Guid("1ed3e918-43fe-41d8-ab97-faa9c2c18e81"), "Good", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9224), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), 5, null, "8d264581-742d-41be-bd07-78c0453e875e", null },
                    { new Guid("2eb44b50-03dc-4260-9112-e4f5dd5b58b1"), "Excellent value for money!", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9239), new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), 5, null, "821ea039-ebbf-4105-a966-065738232bbf", null },
                    { new Guid("5d936bec-d762-4e71-bebb-d0b3e93b2604"), "The product arrived damaged.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9254), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), 1, null, "415a79a3-ea4f-4bb7-a6dc-9abf82cba517", null },
                    { new Guid("870c5f78-635d-4d3c-8743-6503cda5313f"), "Not worth the price.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9244), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), 2, null, "c28a969e-7d9d-4375-935c-94fd7a32447b", null },
                    { new Guid("c72b4e21-2132-4e8e-956c-264521b67399"), "Not good", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9234), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), 3, null, "415a79a3-ea4f-4bb7-a6dc-9abf82cba517", null },
                    { new Guid("cba0c0b7-5861-4170-acdd-448df80df222"), "Great customer service.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9249), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), 4, null, "8d264581-742d-41be-bd07-78c0453e875e", null }
                });

            migrationBuilder.InsertData(
                table: "Filters",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Name", "SpecificationId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("5f657eb3-0e5a-4d92-9e62-511eb5eca44e"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9021), "128GB Storage", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9022) },
                    { new Guid("7925a857-398f-4b7f-a7d0-a01904e05e6e"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9032), "256GB Storage", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9033) },
                    { new Guid("7b15e343-ae04-40d1-9950-0d2e0e23c343"), new Guid("9fec396e-94c7-426b-a79a-f9345ea94de4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9037), "OLED Display", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9037) },
                    { new Guid("7da27815-7ec1-4747-b42d-a04ac778c62c"), new Guid("08316261-0d65-42da-a033-0320c202bbfa"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9052), "Heart Rate Monitor", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9053) },
                    { new Guid("9790b22f-453f-4374-8d34-951b2be08d0e"), new Guid("9fec396e-94c7-426b-a79a-f9345ea94de4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9043), "5G Connectivity", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9044) },
                    { new Guid("bde49d4d-4693-4315-b7f3-4441e690c411"), new Guid("3ed05c58-1153-40d5-8bea-4f37757eaee4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9061), "4K UHD", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9062) },
                    { new Guid("e2f32831-c697-42f3-aeec-de4088d64477"), new Guid("5e841f78-2a03-489d-84f0-68dbb3c517ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9027), "8GB RAM", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9028) },
                    { new Guid("e70d04c4-c1c7-45d4-aa33-a7906a492742"), new Guid("08316261-0d65-42da-a033-0320c202bbfa"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9047), "Water Resistant", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9048) },
                    { new Guid("eb1a7e41-fe30-4fad-80ac-310243d276f7"), new Guid("1c0a8365-f773-447f-bd57-d077c0d35bfe"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9007), "4GB RAM", new Guid("948251d0-8263-4e6e-979f-c870aac3076f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9009) }
                });

            migrationBuilder.InsertData(
                table: "ProductHardwares",
                columns: new[] { "Id", "CreatedAt", "Name", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0a4b39f9-2bf6-4b2b-a4e0-65e6efd7cb10"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8019), "64GB Storage", new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8020) },
                    { new Guid("0cc8afed-461d-45bb-a6a8-9ea6d5ff5ab3"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8174), "Customizable Watch Faces", new Guid("60072ca9-8a74-4244-b219-973e8c65b7c8"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8175) },
                    { new Guid("0d7e5182-9274-4240-a7c5-fd638b5e2c77"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8182), "Convertible Design", new Guid("20d019d7-56f5-481e-8419-2fe708b8f50b"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8183) },
                    { new Guid("21bfe7f5-b765-4b73-97c4-e00657449f02"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8070), "Intel i7 Processor", new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8071) },
                    { new Guid("2c4e78e8-ab49-417d-8df2-29ce4f0a94b0"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8169), "128GB Storage", new Guid("159ff9d4-b243-4681-a22e-39e1702f5bd0"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8170) },
                    { new Guid("390c57fb-093e-45ab-9e65-49034378c995"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8156), "Intel i7 Processor", new Guid("7a47207f-eed6-425b-adb4-6b764de3dc59"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8157) },
                    { new Guid("41e9d353-dbf2-4338-a373-397c3c1936ee"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8134), "Wireless Charging Case", new Guid("03727abf-ffb5-4a6a-85e5-a65e6b7e8b77"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8135) },
                    { new Guid("43139a87-9a8f-49a5-b373-d7db2d67288d"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8033), "256GB Storage", new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8034) },
                    { new Guid("43834ce0-7701-408a-9c77-7cd2b1e5bf88"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8050), "Bluetooth Version", new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8050) },
                    { new Guid("4b94c97b-313f-44d7-adb7-3ddf464729e1"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8093), "NVIDIA RTX 3060", new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8094) },
                    { new Guid("4ca99e42-7ab3-4058-bb34-a956e1bbb603"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8098), "16GB RAM", new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8098) },
                    { new Guid("4e1ed938-1dc9-49c5-9c22-d28e71ebbd05"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8115), "32GB RAM", new Guid("11f1f291-763c-49ad-b4d2-2372dbaa6210"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8116) },
                    { new Guid("5918720d-0f65-490b-91a7-10b1e8ab0d32"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8103), "1TB Storage", new Guid("fbfb9d1d-ac03-442d-8d95-43e82ffb9297"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8104) },
                    { new Guid("6c0ba97c-18d4-47bc-b682-0dad28a827c8"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8074), "16GB RAM", new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8075) },
                    { new Guid("70d7ffee-588c-452b-8812-398253561903"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8038), "128GB Storage", new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8039) },
                    { new Guid("785ed81d-a45c-4b24-9479-1303c25a3c23"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8138), "Active Noise Cancellation", new Guid("8e92b5d8-3a67-48cb-b11b-5b781d2e85df"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8139) },
                    { new Guid("7c1c6736-b949-4331-97c2-322bad4eb3a5"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8088), "8GB RAM", new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8089) },
                    { new Guid("7f50a22b-4604-45f5-bd91-b0532572b9b4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8152), "32GB RAM", new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8153) },
                    { new Guid("80dc6aaa-677b-4224-9ce1-8fe14a525b80"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8083), "AMD Ryzen 5 Processor", new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8084) },
                    { new Guid("8a0a019f-8bda-4673-a0a4-c633b82526ac"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8161), "128GB Storage", new Guid("d6efaed7-35b4-4344-b71d-6e50cea8caf4"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8162) },
                    { new Guid("98f027de-2fba-49b1-aad0-9413a4eda49c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8056), "LTE Version", new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8057) },
                    { new Guid("9fa8b71a-5926-49b4-8ca3-d2c7102a563e"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8044), "256GB Storage", new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8045) },
                    { new Guid("aca15a45-4cc6-41cd-b5fa-64ede86dd966"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8108), "AMD Ryzen 9 Processor", new Guid("11f1f291-763c-49ad-b4d2-2372dbaa6210"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8109) },
                    { new Guid("b2b72430-5dde-405c-a4f6-6e0a35e55022"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8121), "Intel i9 Processor", new Guid("6cb3acc9-d11f-46a6-8fdb-58bc377a9f02"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8122) },
                    { new Guid("b30ca3f5-0d9d-4627-8bca-c27279de4e58"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8025), "128GB Storage", new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8026) },
                    { new Guid("c2cd7446-c5cd-4b13-9d59-e43ab64d2c38"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8129), "256GB Storage", new Guid("d37bb274-8a87-4682-9acb-f07650b04918"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8130) },
                    { new Guid("c93e5f6b-c112-49dc-ae34-9c12caf54b31"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8143), "Industry-leading Noise Cancellation", new Guid("8801342a-9f51-47df-827d-9d98f0962882"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8144) },
                    { new Guid("cc04a91d-1d7d-43e9-a18e-ff812355f886"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8006), "256GB Storage", new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8007) },
                    { new Guid("f0802d8e-c8c4-47e0-95d7-89eb1e92e5ff"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8147), "Intel i9 Processor", new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8148) },
                    { new Guid("f37b4ffb-0249-4d52-b503-437d35b0cd4d"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8061), "4K UHD", new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8062) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "Alt", "CreatedAt", "ProductId", "Title", "Type", "UpdatedAt", "UrlImage" },
                values: new object[,]
                {
                    { new Guid("0061acfe-3c7c-4a33-9d75-ed03830f99be"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8385), new Guid("6cb3acc9-d11f-46a6-8fdb-58bc377a9f02"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8386), "https://example.com/images/acer-predator-slide1.jpg" },
                    { new Guid("05f971e9-8548-4fac-bf09-90bfba4dbbca"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8393), new Guid("6cb3acc9-d11f-46a6-8fdb-58bc377a9f02"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8394), "https://example.com/images/acer-predator-content.jpg" },
                    { new Guid("05ffbb86-7e1b-42d8-afb3-5a7b024320c1"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8403), new Guid("d37bb274-8a87-4682-9acb-f07650b04918"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8404), "https://example.com/images/huawei-mate40-content.jpg" },
                    { new Guid("0e0f01e0-0fcd-4d49-976a-cd5b757f5bc8"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8418), new Guid("8e92b5d8-3a67-48cb-b11b-5b781d2e85df"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8419), "https://example.com/images/galaxy-buds-slide1.jpg" },
                    { new Guid("1055ea4f-dd5a-416e-8ed3-c8d495f2353c"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8205), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8206), "https://example.com/images/iphone13-slide1.jpg" },
                    { new Guid("10b2179e-16cc-421c-9674-6a9744826c2a"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8459), new Guid("7a47207f-eed6-425b-adb4-6b764de3dc59"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8460), "https://example.com/images/hp-envy15-content.jpg" },
                    { new Guid("11321afc-c531-4dd8-9a7d-b6fe8132ec9e"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8217), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8218), "https://example.com/images/iphone13-content.jpg" },
                    { new Guid("1cb5619d-6afe-4fc2-8662-4e2cd3e98d11"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8442), new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8443), "https://example.com/images/alienware-slide1.jpg" },
                    { new Guid("3798e0aa-bfb3-44fd-823e-70c4e7b535a2"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8263), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8264), "https://example.com/images/galaxy-watch-content.jpg" },
                    { new Guid("4596fb80-35c9-4b43-b2f9-8b63f09fa50b"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8448), new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8449), "https://example.com/images/alienware-content.jpg" },
                    { new Guid("4fefa797-ab7b-4f8e-bdc8-18ad2d7a1bb9"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8223), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8224), "https://example.com/images/galaxy-s21-slide1.jpg" },
                    { new Guid("5097edf0-2714-4dd5-980c-d03cdb1ab9d5"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8380), new Guid("11f1f291-763c-49ad-b4d2-2372dbaa6210"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8381), "https://example.com/images/asus-rog-content.jpg" },
                    { new Guid("525940cf-89b1-49d2-b5dd-bf1bc20240d1"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8283), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8286), "https://example.com/images/dell-xps13-slide1.jpg" },
                    { new Guid("54fdb79c-a80d-4f89-86c9-8d1379016af4"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8484), new Guid("159ff9d4-b243-4681-a22e-39e1702f5bd0"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8485), "https://example.com/images/apple-tv4k-content.jpg" },
                    { new Guid("5530eea7-398b-4333-845e-fa7f46187821"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8370), new Guid("fbfb9d1d-ac03-442d-8d95-43e82ffb9297"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8371), "https://example.com/images/xbox-series-x-content.jpg" },
                    { new Guid("584bbadc-af2a-45f1-854e-80a3b9db737f"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8453), new Guid("7a47207f-eed6-425b-adb4-6b764de3dc59"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8454), "https://example.com/images/hp-envy15-slide1.jpg" },
                    { new Guid("60ff31eb-f376-435a-9f88-465c2de37f5f"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8465), new Guid("d6efaed7-35b4-4344-b71d-6e50cea8caf4"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8466), "https://example.com/images/razer-blade15-slide1.jpg" },
                    { new Guid("68f9d27a-39b1-42ce-8dd1-abd22c116b81"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8428), new Guid("8801342a-9f51-47df-827d-9d98f0962882"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8430), "https://example.com/images/sony-wh1000xm4-slide1.jpg" },
                    { new Guid("779aa884-8e8e-4f66-9bef-dda45b2fe8b4"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8354), new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8355), "https://example.com/images/lenovo-legion5-slide1.jpg" },
                    { new Guid("7b50af32-9a76-494b-b26e-2f330e30cd48"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8398), new Guid("d37bb274-8a87-4682-9acb-f07650b04918"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8399), "https://example.com/images/huawei-mate40-slide1.jpg" },
                    { new Guid("8f834b35-00d0-4250-9d85-3f67afd9e717"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8291), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8292), "https://example.com/images/dell-xps13-content.jpg" },
                    { new Guid("9539818b-1eca-4753-98e5-3cbfdca9a8e8"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8296), new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8297), "https://example.com/images/hp-pavilion-slide1.jpg" },
                    { new Guid("9614a04b-97a6-49d4-8936-271e188ef251"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8488), new Guid("60072ca9-8a74-4244-b219-973e8c65b7c8"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8489), "https://example.com/images/xbox-series-s-slide1.jpg" },
                    { new Guid("9a521634-a63c-4431-9614-0ddc3a2bc8c5"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8470), new Guid("d6efaed7-35b4-4344-b71d-6e50cea8caf4"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8471), "https://example.com/images/razer-blade15-content.jpg" },
                    { new Guid("9eec9a44-1aac-4442-bd16-723c9dc21bdc"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8375), new Guid("11f1f291-763c-49ad-b4d2-2372dbaa6210"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8376), "https://example.com/images/asus-rog-slide1.jpg" },
                    { new Guid("9f344ec9-ea50-42d7-b5e2-c4e71b771b20"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8269), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8270), "https://example.com/images/sony-bravia-slide1.jpg" },
                    { new Guid("a63f3357-c5d2-4d63-b324-f229d057eb67"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8364), new Guid("fbfb9d1d-ac03-442d-8d95-43e82ffb9297"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8365), "https://example.com/images/xbox-series-x-slide1.jpg" },
                    { new Guid("a8729ee5-f46c-43d4-a0f9-a92b6683ca0f"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8239), new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8240), "https://example.com/images/ipad-pro-slide1.jpg" },
                    { new Guid("afe2ca34-7a43-46e7-94f9-502369afcd0a"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8496), new Guid("60072ca9-8a74-4244-b219-973e8c65b7c8"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8497), "https://example.com/images/xbox-series-s-content.jpg" },
                    { new Guid("b67cfc4a-7f15-40af-a853-51372840e336"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8437), new Guid("8801342a-9f51-47df-827d-9d98f0962882"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8438), "https://example.com/images/sony-wh1000xm4-content.jpg" },
                    { new Guid("b6d46850-eb8b-41d7-aaab-5dccf0eaf833"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8258), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8259), "https://example.com/images/galaxy-watch-slide1.jpg" },
                    { new Guid("c192720c-962c-4d51-ba36-c766e5638121"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8278), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8279), "https://example.com/images/sony-bravia-content.jpg" },
                    { new Guid("c713debe-bcca-4c44-971c-391c46a115a2"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8359), new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8360), "https://example.com/images/lenovo-legion5-content.jpg" },
                    { new Guid("c7432ebb-c14f-4a26-a5a5-3a95d5736be6"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8249), new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8250), "https://example.com/images/ipad-pro-content.jpg" },
                    { new Guid("de49dcb1-5f3a-464b-9613-b86f3303d057"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8475), new Guid("159ff9d4-b243-4681-a22e-39e1702f5bd0"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8476), "https://example.com/images/apple-tv4k-slide1.jpg" },
                    { new Guid("e7621f0b-58a9-490a-8a98-5faf2b6876bc"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8228), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8229), "https://example.com/images/galaxy-s21-content.jpg" },
                    { new Guid("e9076cf2-ac48-4db8-85c8-2e59c13d814c"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8349), new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8350), "https://example.com/images/hp-pavilion-content.jpg" },
                    { new Guid("fb21a2bf-b0d9-4c80-a5a9-2a07563b02ce"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8424), new Guid("8e92b5d8-3a67-48cb-b11b-5b781d2e85df"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8424), "https://example.com/images/galaxy-buds-content.jpg" },
                    { new Guid("fc83d275-f09f-4857-b9fb-d5a17c7bb48e"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8408), new Guid("03727abf-ffb5-4a6a-85e5-a65e6b7e8b77"), null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8409), "https://example.com/images/airpods-pro-slide1.jpg" },
                    { new Guid("fd0b5495-418a-4c9c-977a-08fbaf3aa258"), null, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8414), new Guid("03727abf-ffb5-4a6a-85e5-a65e6b7e8b77"), null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8415), "https://example.com/images/airpods-pro-content.jpg" }
                });

            migrationBuilder.InsertData(
                table: "invoices",
                columns: new[] { "Id", "Address", "CollaboratorId", "CreatedAt", "CustomerId", "MethodPaymment", "Name", "Note", "Status", "Telephone", "Total", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("04451aa3-4eb6-47d5-86aa-0cb265f76a5e"), "San Francisco, USA", new Guid("d7bdf57d-2249-4de5-be3f-3509ee5e6e7d"), new DateTime(2024, 9, 20, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8866), new Guid("7e63353d-082c-403a-b8ec-88302752b402"), "Bank Transfer", "John Doe", null, 3, "123456789", null, null },
                    { new Guid("76d08cc3-fece-48a3-91a3-197e87c291eb"), "Los Angeles, USA", new Guid("d7bdf57d-2249-4de5-be3f-3509ee5e6e7d"), new DateTime(2024, 9, 25, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8854), new Guid("7e63353d-082c-403a-b8ec-88302752b402"), "PayPal", "Jane Doe", null, 2, "987654321", null, null },
                    { new Guid("e66f82d9-7fed-44ad-8ae2-3d9f409a17e1"), "New York, USA", new Guid("d7bdf57d-2249-4de5-be3f-3509ee5e6e7d"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8835), new Guid("7e63353d-082c-403a-b8ec-88302752b402"), "Credit Card", "John Doe", null, 1, "123456789", null, null }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentText", "CreatedAt", "ProductId", "Rate", "UpdatedAt", "UserId", "UserReplyId" },
                values: new object[,]
                {
                    { new Guid("11e6a718-6d09-49b0-aab6-fab994490705"), "I agree with you!", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9566), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), 0, null, "415a79a3-ea4f-4bb7-a6dc-9abf82cba517", new Guid("1ed3e918-43fe-41d8-ab97-faa9c2c18e81") },
                    { new Guid("2465da2f-cacc-4252-9352-ba6c44e2dd75"), "I agree, it's a great product.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9650), new Guid("6f85c21e-7562-49ec-967c-1259850f0197"), 0, null, "c28a969e-7d9d-4375-935c-94fd7a32447b", new Guid("2eb44b50-03dc-4260-9112-e4f5dd5b58b1") },
                    { new Guid("2d730a3e-f11f-47d2-9e2d-649cc75b1117"), "I had a different experience with the service.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9704), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), 0, null, "821ea039-ebbf-4105-a966-065738232bbf", new Guid("cba0c0b7-5861-4170-acdd-448df80df222") },
                    { new Guid("33bddd93-c688-4a64-af9a-62aefe1be959"), "Did you contact support?", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9745), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), 0, null, "8d264581-742d-41be-bd07-78c0453e875e", new Guid("5d936bec-d762-4e71-bebb-d0b3e93b2604") },
                    { new Guid("567a2fb2-42f0-465c-84c8-af02cbf589dd"), "I had a similar issue.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9637), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), 0, null, "821ea039-ebbf-4105-a966-065738232bbf", new Guid("c72b4e21-2132-4e8e-956c-264521b67399") },
                    { new Guid("5bbd82a6-c38f-49ed-ba0b-0dd8d7a9fd1e"), "It depends on what you're looking for.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9687), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), 0, null, "8d264581-742d-41be-bd07-78c0453e875e", new Guid("870c5f78-635d-4d3c-8743-6503cda5313f") },
                    { new Guid("62fd06b3-9081-473e-8b3f-2d84231bf3e5"), "Why do you think so?", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9624), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae"), 0, null, "8d264581-742d-41be-bd07-78c0453e875e", new Guid("c72b4e21-2132-4e8e-956c-264521b67399") },
                    { new Guid("b1907f3b-b7c2-49c2-8968-a1a61760126e"), "I had a different experience.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9612), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f"), 0, null, "821ea039-ebbf-4105-a966-065738232bbf", new Guid("1ed3e918-43fe-41d8-ab97-faa9c2c18e81") },
                    { new Guid("b7bc095e-aea2-49f8-bcd1-819d11f84946"), "I thought it was okay.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9667), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a"), 0, null, "415a79a3-ea4f-4bb7-a6dc-9abf82cba517", new Guid("870c5f78-635d-4d3c-8743-6503cda5313f") },
                    { new Guid("df908308-f869-4410-a648-771669e65b49"), "I had the same issue. They sent a replacement quickly.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9777), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc"), 0, null, "c28a969e-7d9d-4375-935c-94fd7a32447b", new Guid("5d936bec-d762-4e71-bebb-d0b3e93b2604") },
                    { new Guid("e36195f3-9ab0-4cfd-bf92-8eb85d140b5f"), "They were very helpful for me too.", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9721), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8"), 0, null, "c28a969e-7d9d-4375-935c-94fd7a32447b", new Guid("cba0c0b7-5861-4170-acdd-448df80df222") }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "CreateAt", "IsCustomer", "Text_message" },
                values: new object[,]
                {
                    { new Guid("1d6dc86e-4821-4205-9b1e-9450bddc0ec0"), new Guid("3d7bc032-2950-4d50-9502-028d1b6c8341"), new DateTime(2024, 9, 30, 10, 28, 36, 315, DateTimeKind.Local).AddTicks(9882), true, "Hello" },
                    { new Guid("4d03dd4e-1b0b-458f-bcc6-7aeda12cb491"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 26, 36, 315, DateTimeKind.Local).AddTicks(9873), true, "Yes, i working like a dog day and day, and he not allow me rest, alse the holiday he assign so many task to me" },
                    { new Guid("5d8ed3b4-defb-401f-a294-555b1699f680"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 27, 36, 315, DateTimeKind.Local).AddTicks(9878), false, "Im sorry about that, i hear so many the complain about your boss" },
                    { new Guid("7bf01183-026b-479c-9fbe-75ddceba118d"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 25, 36, 315, DateTimeKind.Local).AddTicks(9865), false, "Your boss is Nguyen Phuc Du" },
                    { new Guid("a7b9773e-43ec-4ae6-890a-2040515cff33"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 24, 36, 315, DateTimeKind.Local).AddTicks(9861), true, "Im fine, but my boss at FPT 'Chen ep' me, i can't work at the company anymore" },
                    { new Guid("c67ee899-e612-4a58-875c-4df6a3b0ab76"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 23, 36, 315, DateTimeKind.Local).AddTicks(9853), false, "Hello, how are you today" },
                    { new Guid("ed23d914-607c-4fa4-a91b-cebb23637454"), new Guid("3d7bc032-2950-4d50-9502-028d1b6c8341"), new DateTime(2024, 9, 30, 10, 29, 36, 315, DateTimeKind.Local).AddTicks(9886), false, "F*ck you" },
                    { new Guid("faa0685b-c6f3-4bcb-8ed8-3ad045e19674"), new Guid("8e5e9094-ec27-4c57-b5f1-11827cbe74bf"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9841), true, "Hello you" }
                });

            migrationBuilder.InsertData(
                table: "ProductColors",
                columns: new[] { "Id", "CreatedAt", "Price", "ProductHardwareId", "Quantity", "RGB", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("145570c3-63a2-405f-90d9-e6ad9d3b0ad0"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8592), 999.99000000000001, new Guid("43139a87-9a8f-49a5-b373-d7db2d67288d"), 45, "#8C4B9A", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8593) },
                    { new Guid("224a51b6-6621-4583-aecf-5efad1b51ecd"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8691), 1299.99, new Guid("f37b4ffb-0249-4d52-b503-437d35b0cd4d"), 40, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8692) },
                    { new Guid("2570da2a-3417-4ddb-8495-ee1fd82f5c8e"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8541), 999.99000000000001, new Guid("cc04a91d-1d7d-43e9-a18e-ff812355f886"), 30, "#FF0000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8542) },
                    { new Guid("309730d6-9b36-40d6-abf4-a57a438788d1"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8572), 849.99000000000001, new Guid("b30ca3f5-0d9d-4627-8bca-c27279de4e58"), 65, "#F8F8F8", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8572) },
                    { new Guid("309f4640-5110-4f27-a086-7c83a355b47a"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8547), 799.99000000000001, new Guid("0a4b39f9-2bf6-4b2b-a4e0-65e6efd7cb10"), 60, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8548) },
                    { new Guid("386bcdf5-753e-49fc-9f5b-a2acc9eee252"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8686), 1399.99, new Guid("98f027de-2fba-49b1-aad0-9413a4eda49c"), 30, "#F7F7F7", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8687) },
                    { new Guid("3b4b280a-58c5-4e09-ad8c-6eb1b49269ad"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8658), 1199.99, new Guid("43834ce0-7701-408a-9c77-7cd2b1e5bf88"), 30, "#C0C0C0", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8659) },
                    { new Guid("3d38f2f6-f485-4363-9f86-b87e7f74efd5"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8566), 849.99000000000001, new Guid("b30ca3f5-0d9d-4627-8bca-c27279de4e58"), 70, "#6D6E71", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8567) },
                    { new Guid("3eb5f024-6d98-4bc5-ab11-789e5cc55139"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8536), 999.99000000000001, new Guid("cc04a91d-1d7d-43e9-a18e-ff812355f886"), 40, "#FFFFFF", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8537) },
                    { new Guid("43490e1c-34ab-47b5-9e2f-8938e3f6fc86"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8791), 1499.99, new Guid("7c1c6736-b949-4331-97c2-322bad4eb3a5"), 12, "#343434", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8792) },
                    { new Guid("4a3cca15-4a27-4815-8dd0-d34c59cf3d0c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8676), 1199.99, new Guid("43834ce0-7701-408a-9c77-7cd2b1e5bf88"), 25, "#F7F7F7", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8677) },
                    { new Guid("51f944d5-d6bc-49a4-bab2-a2a0f757cc17"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8653), 999.99000000000001, new Guid("9fa8b71a-5926-49b4-8ca3-d2c7102a563e"), 70, "#C0C0C0", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8654) },
                    { new Guid("529c1927-2952-4e80-8a0b-ba9c6bf3dc7c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8750), 499.99000000000001, new Guid("21bfe7f5-b765-4b73-97c4-e00657449f02"), 50, "#8B8B8B", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8751) },
                    { new Guid("6304aaa8-780f-4cb6-ac3d-771be102f7e3"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8681), 1399.99, new Guid("98f027de-2fba-49b1-aad0-9413a4eda49c"), 35, "#C0C0C0", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8682) },
                    { new Guid("6f9a64ff-6d8d-479a-b987-b8b8b7605c0b"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8797), 1199.99, new Guid("4b94c97b-313f-44d7-adb7-3ddf464729e1"), 10, "#FFFFFF", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8798) },
                    { new Guid("7ddffeea-ca75-430d-b35b-f2530c157423"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8765), 1199.99, new Guid("6c0ba97c-18d4-47bc-b682-0dad28a827c8"), 20, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8766) },
                    { new Guid("7fa46d69-e12f-46c5-b5bd-5d83ef43e2ac"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8577), 849.99000000000001, new Guid("b30ca3f5-0d9d-4627-8bca-c27279de4e58"), 60, "#8C4B9A", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8578) },
                    { new Guid("80e8e6a1-66c6-430f-87c5-9414f8e3bf56"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8809), 1199.99, new Guid("4b94c97b-313f-44d7-adb7-3ddf464729e1"), 8, "#7B7B7B", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8810) },
                    { new Guid("81c505f7-df93-4beb-956d-2b4574a42333"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8755), 499.99000000000001, new Guid("21bfe7f5-b765-4b73-97c4-e00657449f02"), 45, "#FFFFFF", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8756) },
                    { new Guid("8291575a-8d14-416d-9175-42c4f558aa11"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8606), 799.99000000000001, new Guid("70d7ffee-588c-452b-8812-398253561903"), 80, "#C0C0C0", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8606) },
                    { new Guid("8aa53a8f-6358-4b16-aa7c-640f02bd26f5"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8517), 999.99000000000001, new Guid("cc04a91d-1d7d-43e9-a18e-ff812355f886"), 50, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8518) },
                    { new Guid("9b35fd7d-362d-4058-849e-22a97b4cdeb6"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8745), 1299.99, new Guid("f37b4ffb-0249-4d52-b503-437d35b0cd4d"), 30, "#CCCCCC", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8745) },
                    { new Guid("c17b0f1d-88c4-49b6-b303-4a80530dc612"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8611), 999.99000000000001, new Guid("9fa8b71a-5926-49b4-8ca3-d2c7102a563e"), 75, "#3C3C3C", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8612) },
                    { new Guid("c4db2e52-45a0-4fb2-929f-34cd3c995bf7"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8760), 499.99000000000001, new Guid("21bfe7f5-b765-4b73-97c4-e00657449f02"), 40, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8761) },
                    { new Guid("d34c080f-e6e8-43d3-b894-80f05d5e00fd"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8587), 999.99000000000001, new Guid("43139a87-9a8f-49a5-b373-d7db2d67288d"), 50, "#F8F8F8", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8589) },
                    { new Guid("d77d777c-166a-42b4-af2f-621efcb08a4c"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8774), 499.99000000000001, new Guid("80dc6aaa-677b-4224-9ce1-8fe14a525b80"), 25, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8775) },
                    { new Guid("daff0a1c-5115-4b43-b2bc-11996ae3b1ca"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8769), 1199.99, new Guid("6c0ba97c-18d4-47bc-b682-0dad28a827c8"), 18, "#4B4B4B", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8770) },
                    { new Guid("dea51443-517c-4ab4-962f-49d496d95d93"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8786), 1499.99, new Guid("7c1c6736-b949-4331-97c2-322bad4eb3a5"), 15, "#000000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8787) },
                    { new Guid("e0040ac1-980c-4d1a-942a-9c4ce6eea190"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8582), 999.99000000000001, new Guid("43139a87-9a8f-49a5-b373-d7db2d67288d"), 55, "#6D6E71", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8583) },
                    { new Guid("e07e09e0-631f-4a57-a653-b806d3d98c84"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8562), 799.99000000000001, new Guid("0a4b39f9-2bf6-4b2b-a4e0-65e6efd7cb10"), 45, "#FF0000", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8563) },
                    { new Guid("e680730b-e4d1-4b92-8562-c9abae6a3b04"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8597), 799.99000000000001, new Guid("70d7ffee-588c-452b-8812-398253561903"), 85, "#3C3C3C", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8598) },
                    { new Guid("f9412360-4395-4656-9ac1-d99a04c540ba"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8700), 1299.99, new Guid("f37b4ffb-0249-4d52-b503-437d35b0cd4d"), 35, "#FFFFFF", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8701) },
                    { new Guid("fe569fd5-81ec-4657-b4c1-22b3871a2124"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8552), 799.99000000000001, new Guid("0a4b39f9-2bf6-4b2b-a4e0-65e6efd7cb10"), 50, "#FFFFFF", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8553) },
                    { new Guid("fe7a3eda-d031-4b8c-8411-3787c79ba11b"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8782), 499.99000000000001, new Guid("80dc6aaa-677b-4224-9ce1-8fe14a525b80"), 20, "#4F4F4F", new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(8782) }
                });

            migrationBuilder.InsertData(
                table: "ProductFilters",
                columns: new[] { "FilterId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("5f657eb3-0e5a-4d92-9e62-511eb5eca44e"), new Guid("838697de-b0d3-41b1-b147-cb650b0799ae") },
                    { new Guid("5f657eb3-0e5a-4d92-9e62-511eb5eca44e"), new Guid("d37bb274-8a87-4682-9acb-f07650b04918") },
                    { new Guid("7925a857-398f-4b7f-a7d0-a01904e05e6e"), new Guid("8e92b5d8-3a67-48cb-b11b-5b781d2e85df") },
                    { new Guid("7925a857-398f-4b7f-a7d0-a01904e05e6e"), new Guid("963c9e39-a3a3-4423-864c-58d5f73c174a") },
                    { new Guid("7b15e343-ae04-40d1-9950-0d2e0e23c343"), new Guid("79e46632-4787-438a-b0d7-3289d4fcebb8") },
                    { new Guid("7b15e343-ae04-40d1-9950-0d2e0e23c343"), new Guid("8801342a-9f51-47df-827d-9d98f0962882") },
                    { new Guid("7da27815-7ec1-4747-b42d-a04ac778c62c"), new Guid("d59af16d-2581-49ab-b6aa-2ea2e6563ae4") },
                    { new Guid("7da27815-7ec1-4747-b42d-a04ac778c62c"), new Guid("d6efaed7-35b4-4344-b71d-6e50cea8caf4") },
                    { new Guid("9790b22f-453f-4374-8d34-951b2be08d0e"), new Guid("81db7cf2-516c-4ea4-a622-4ab5574498bc") },
                    { new Guid("9790b22f-453f-4374-8d34-951b2be08d0e"), new Guid("e3d0f57d-4323-4b31-92cb-d968a394a3a0") },
                    { new Guid("bde49d4d-4693-4315-b7f3-4441e690c411"), new Guid("159ff9d4-b243-4681-a22e-39e1702f5bd0") },
                    { new Guid("bde49d4d-4693-4315-b7f3-4441e690c411"), new Guid("fbfb9d1d-ac03-442d-8d95-43e82ffb9297") },
                    { new Guid("e2f32831-c697-42f3-aeec-de4088d64477"), new Guid("03727abf-ffb5-4a6a-85e5-a65e6b7e8b77") },
                    { new Guid("e2f32831-c697-42f3-aeec-de4088d64477"), new Guid("6f85c21e-7562-49ec-967c-1259850f0197") },
                    { new Guid("e70d04c4-c1c7-45d4-aa33-a7906a492742"), new Guid("6a3592ed-a136-43c9-ba49-540a621fd7a6") },
                    { new Guid("e70d04c4-c1c7-45d4-aa33-a7906a492742"), new Guid("7a47207f-eed6-425b-adb4-6b764de3dc59") },
                    { new Guid("eb1a7e41-fe30-4fad-80ac-310243d276f7"), new Guid("6cb3acc9-d11f-46a6-8fdb-58bc377a9f02") },
                    { new Guid("eb1a7e41-fe30-4fad-80ac-310243d276f7"), new Guid("97622d2b-8ce7-4c41-ba15-7443e5c6e48f") }
                });

            migrationBuilder.InsertData(
                table: "InvoiceProducts",
                columns: new[] { "InvoiceId", "ProductColorsId", "CreatedAt", "ProductPrice", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("04451aa3-4eb6-47d5-86aa-0cb265f76a5e"), new Guid("e07e09e0-631f-4a57-a653-b806d3d98c84"), null, 1199.99, 1, null },
                    { new Guid("04451aa3-4eb6-47d5-86aa-0cb265f76a5e"), new Guid("fe569fd5-81ec-4657-b4c1-22b3871a2124"), null, 1199.99, 1, null },
                    { new Guid("76d08cc3-fece-48a3-91a3-197e87c291eb"), new Guid("2570da2a-3417-4ddb-8495-ee1fd82f5c8e"), null, 849.99000000000001, 1, null },
                    { new Guid("76d08cc3-fece-48a3-91a3-197e87c291eb"), new Guid("309f4640-5110-4f27-a086-7c83a355b47a"), null, 999.99000000000001, 3, null },
                    { new Guid("e66f82d9-7fed-44ad-8ae2-3d9f409a17e1"), new Guid("3eb5f024-6d98-4bc5-ab11-789e5cc55139"), null, 799.99000000000001, 2, null },
                    { new Guid("e66f82d9-7fed-44ad-8ae2-3d9f409a17e1"), new Guid("8aa53a8f-6358-4b16-aa7c-640f02bd26f5"), null, 999.99000000000001, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "ProductColorId", "CreatedAt", "OrderCustomerId", "OrderProductColorId", "Quanitity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), new Guid("2570da2a-3417-4ddb-8495-ee1fd82f5c8e"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9191), null, null, 3, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9192) },
                    { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), new Guid("309f4640-5110-4f27-a086-7c83a355b47a"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9198), null, null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9199) },
                    { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), new Guid("3eb5f024-6d98-4bc5-ab11-789e5cc55139"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9188), null, null, 1, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9188) },
                    { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), new Guid("8aa53a8f-6358-4b16-aa7c-640f02bd26f5"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9179), null, null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9180) },
                    { new Guid("7e63353d-082c-403a-b8ec-88302752b402"), new Guid("fe569fd5-81ec-4657-b4c1-22b3871a2124"), new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9201), null, null, 2, new DateTime(2024, 9, 30, 10, 22, 36, 315, DateTimeKind.Local).AddTicks(9202) }
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
