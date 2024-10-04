using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Data
{
	public class TechshopDbContext : IdentityDbContext<User>
	{
		public override DbSet<User> Users { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Collaborator> Collaborators { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Filter> Filters { get; set; }
		public DbSet<Invoice> invoices { get; set; }
		public DbSet<InvoiceProductColors> InvoiceProducts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<ProductHardware> ProductHardwares { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductFilter> ProductFilters { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Specification> Specifications { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<Message> Messages { get; set; }
		public TechshopDbContext(DbContextOptions<TechshopDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            builder.Entity<Customer>()
					.HasMany(c => c.ProductColors)
					.WithMany(p => p.Customers)
					.UsingEntity<Order>(
						p => p.HasOne(p => p.ProductColor).WithMany(p => p.Orders).HasForeignKey(p => p.ProductColorId),
						c => c.HasOne(c => c.Customer).WithMany(c => c.Orders).HasForeignKey(c => c.CustomerId)
				);

			builder.Entity<Invoice>()
					.HasMany(c => c.ProductColors)
					.WithMany(p => p.Invoices)
					.UsingEntity<InvoiceProductColors>(
						p => p.HasOne(p => p.ProductColor).WithMany(p => p.InvoiceProducts).HasForeignKey(p => p.ProductColorsId),
						i => i.HasOne(i => i.Invoice).WithMany(i => i.InvoiceProducts).HasForeignKey(i => i.InvoiceId)
				);

			builder.Entity<Product>()
					.HasMany(p => p.Filters)
					.WithMany(p => p.Products)
					.UsingEntity<ProductFilter>(
						j => j.HasOne(p => p.Filter).WithMany(p => p.ProductFilters).HasForeignKey(p => p.FilterId).OnDelete(DeleteBehavior.Restrict),
						j => j.HasOne(p => p.Product).WithMany(p => p.ProductFilters).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict)
					);

			builder.Entity<Category>()
			.HasMany(c => c.Products)
			.WithOne(p => p.Category)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Product>()
			.HasMany(c => c.ProductFilters)
			.WithOne(p => p.Product)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Product>()
			.HasMany(c => c.ProductHardwares)
			.WithOne(p => p.Product)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Product>()
			.HasMany(c => c.Images)
			.WithOne(p => p.Product)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Product>()
			.HasMany(c => c.Comments)
			.WithOne(p => p.Product)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<ProductHardware>()
			.HasMany(c => c.ProductColors)
			.WithOne(p => p.ProductHardware)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Category>()
			.HasMany(c => c.Filters)
			.WithOne(p => p.Category)
			.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Invoice>()
				.HasOne(c => c.Collaborator)
				.WithOne(c => c.Invoice)
                .HasForeignKey<Collaborator>(c => c.InvoiceId);

            SeedData(builder);
		}
		private void SeedData(ModelBuilder modelBuilder)
		{
			var adminRoleId = Guid.NewGuid().ToString();
			var collabRoleId = Guid.NewGuid().ToString();
			var customerRoleId = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{
					Id = adminRoleId,
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
                new IdentityRole
                {
                    Id = collabRoleId,
                    Name = "Collaborator",
                    NormalizedName = "COLLABORATOR"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );


            var users = new List<User>
			{
				new User { Id = Guid.NewGuid().ToString(), PasswordHash = hasher.HashPassword(null, "phucnd20"), UserName = "phucnd20",Email = "phucnd20@fpt.com", FullName = "Nguyen Duy Phuc", Address = "123 Main St, New York, NY", BirthDay = new DateTime(2001, 8, 16), PhoneNumber = "1234567890", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new User { Id = Guid.NewGuid().ToString(), PasswordHash = hasher.HashPassword(null, "khanhtmq2"), UserName = "khanhtmq2",Email = "khanhtmq2@fpt.com", FullName = "Tran Minh Quoc Khanh", Address = "456 Elm St, Los Angeles, CA", BirthDay = new DateTime(2003, 9, 2), PhoneNumber = "9876543210", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new User { Id = Guid.NewGuid().ToString(), PasswordHash = hasher.HashPassword(null, "dattv20"), UserName = "dattv20",Email = "dattv20@fpt.com", FullName = "Tran Van Dat", Address = "789 Pine St, Chicago, IL", BirthDay = new DateTime(2003, 3, 23), PhoneNumber = "5551234567", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new User { Id = Guid.NewGuid().ToString(), PasswordHash = hasher.HashPassword(null, "nhannt96"), UserName = "nhannt96",Email = "nhannt96@fpt.com", FullName = "Nguyen Tri Nhan", Address = "101 Maple St, San Francisco, CA", BirthDay = new DateTime(2002, 4, 30), PhoneNumber = "4449876543", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new User { Id = Guid.NewGuid().ToString(), PasswordHash = hasher.HashPassword(null, "duclt24"), UserName = "duclt24",Email = "duclt24@fpt.com", FullName = "Le Toan Duc", Address = "202 Oak St, Miami, FL", BirthDay = new DateTime(2003, 5, 15), PhoneNumber = "3335678901", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new User { Id = Guid.NewGuid().ToString(), PasswordHash =hasher.HashPassword(null, "anhbhn"), UserName = "anhbhn", Email = "anhbhn@fpt.com", FullName = "Bui Huynh Ngoc Anh", Address = "707 Cherry St, Phoenix, AZ", BirthDay = new DateTime(2002, 02, 02), PhoneNumber = "8886785432", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
			};


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = users[0].Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = collabRoleId,
                    UserId = users[1].Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = collabRoleId,
                    UserId = users[2].Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = collabRoleId,
                    UserId = users[3].Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = customerRoleId,
                    UserId = users[4].Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = customerRoleId,
                    UserId = users[5].Id
                }
            );

            var collaborators = new List<Collaborator>
			{
				new Collaborator { ID = Guid.NewGuid(), UserID = users[1].Id, StartDate = DateTime.Now.AddMonths(-5), EndDate = DateTime.Now.AddMonths(5) },
				new Collaborator { ID = Guid.NewGuid(), UserID = users[2].Id, StartDate = DateTime.Now.AddMonths(-3), EndDate = DateTime.Now.AddMonths(5) },
				new Collaborator { ID = Guid.NewGuid(), UserID = users[3].Id, StartDate = DateTime.Now.AddMonths(-2), EndDate = DateTime.Now.AddMonths(5) },
                new Collaborator { ID = Guid.NewGuid(), UserID = users[4].Id, StartDate = DateTime.Now.AddMonths(-2), EndDate = DateTime.Now.AddMonths(5) },
            };

			var customers = new List<Customer>
			{
				new Customer { ID = Guid.NewGuid(), UserID = users[5].Id, LoyaltyPoints = 150, },
			};

			var admins = new List<Admin>
			{
				new Admin { ID = Guid.NewGuid(), UserID = users[0].Id, AccessLevel = 2 },
			};


			var categories = new List<Category>
			{
				new Category { Id = Guid.NewGuid(), Name = "Phones", UrlSlug = "phones", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Category { Id = Guid.NewGuid(), Name = "Laptops", UrlSlug = "laptops", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Category { Id = Guid.NewGuid(), Name = "Tablets", UrlSlug = "tablets", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Category { Id = Guid.NewGuid(), Name = "Watches", UrlSlug = "watches", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Category { Id = Guid.NewGuid(), Name = "PCs", UrlSlug = "pcs", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Category { Id = Guid.NewGuid(), Name = "TVs", UrlSlug = "tvs", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Category { Id = Guid.NewGuid(), Name = "Accessories", UrlSlug = "accessories", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Category { Id = Guid.NewGuid(), Name = "Audio", UrlSlug = "audio", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Category { Id = Guid.NewGuid(), Name = "Cameras", UrlSlug = "cameras", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
			};

			var brands = new List<Brand>
			{
				new Brand { Id = Guid.NewGuid(), Name = "Apple", UrlSlug = "apple", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Samsung", UrlSlug = "samsung" , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
				new Brand { Id = Guid.NewGuid(), Name = "Sony", UrlSlug = "sony", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Dell", UrlSlug = "dell", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "HP", UrlSlug = "hp", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Lenovo", UrlSlug = "lenovo", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Microsoft", UrlSlug = "microsoft", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Asus", UrlSlug = "asus", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Acer", UrlSlug = "acer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
				new Brand { Id = Guid.NewGuid(), Name = "Huawei", UrlSlug = "huawei", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
			};

		

			var products = new List<Product>
			{
				new Product { Id = Guid.NewGuid(), BrandId = brands[0].Id, CategoryId = categories[0].Id, Name = "iPhone 13", Description = "Latest Apple iPhone with A15 Bionic chip.", Discount = 5, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/iphone13.jpg", UrlSlug = "iphone-13" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[1].Id, CategoryId = categories[1].Id, Name = "Samsung Galaxy S21", Description = "Flagship Samsung phone with Exynos 2100.", Discount = 10, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/galaxy-s21.jpg", UrlSlug = "galaxy-s21" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[0].Id, CategoryId = categories[2].Id, Name = "iPad Pro", Description = "Apple iPad Pro with M1 chip.", Discount = 7, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/ipad-pro.jpg", UrlSlug = "ipad-pro" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[1].Id, CategoryId = categories[3].Id, Name = "Samsung Galaxy Watch", Description = "Smartwatch with advanced health tracking features.", Discount = 12, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/galaxy-watch.jpg", UrlSlug = "galaxy-watch" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[2].Id, CategoryId = categories[5].Id, Name = "Sony Bravia 55-inch", Description = "4K UHD Smart TV with OLED display.", Discount = 15, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/sony-bravia.jpg", UrlSlug = "sony-bravia" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[3].Id, CategoryId = categories[1].Id, Name = "Dell XPS 13", Description = "Compact and powerful ultrabook with Intel i7 processor.", Discount = 10, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/dell-xps13.jpg", UrlSlug = "dell-xps13" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[4].Id, CategoryId = categories[4].Id, Name = "HP Pavilion Desktop", Description = "Affordable desktop with AMD Ryzen processor.", Discount = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/hp-pavilion.jpg", UrlSlug = "hp-pavilion" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[5].Id, CategoryId = categories[8].Id, Name = "Lenovo Legion 5", Description = "High-performance gaming laptop with NVIDIA RTX graphics.", Discount = 18, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/lenovo-legion5.jpg", UrlSlug = "lenovo-legion5" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[6].Id, CategoryId = categories[8].Id, Name = "Microsoft Xbox Series X", Description = "Next-gen gaming console with 4K gaming capabilities.", Discount = 5, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/xbox-series-x.jpg", UrlSlug = "xbox-series-x" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[7].Id, CategoryId = categories[1].Id, Name = "Asus ROG Zephyrus", Description = "Ultra-thin gaming laptop with AMD Ryzen 9 processor.", Discount = 15, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/asus-rog.jpg", UrlSlug = "asus-rog-zephyrus" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[8].Id, CategoryId = categories[4].Id, Name = "Acer Predator Orion 3000", Description = "Compact gaming desktop with powerful performance.", Discount = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/acer-predator.jpg", UrlSlug = "acer-predator-orion" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[9].Id, CategoryId = categories[0].Id, Name = "Huawei Mate 40 Pro", Description = "Flagship Huawei smartphone with cutting-edge technology.", Discount = 10, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/huawei-mate40.jpg", UrlSlug = "huawei-mate40-pro" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[0].Id, CategoryId = categories[6].Id, Name = "Apple AirPods Pro", Description = "Wireless earbuds with active noise cancellation.", Discount = 5, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/airpods-pro.jpg", UrlSlug = "airpods-pro" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[1].Id, CategoryId = categories[7].Id, Name = "Samsung Galaxy Buds Live", Description = "True wireless earbuds with premium sound.", Discount = 8, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/galaxy-buds.jpg", UrlSlug = "galaxy-buds-live" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[2].Id, CategoryId = categories[8].Id, Name = "Sony WH-1000XM4", Description = "Industry-leading noise canceling headphones.", Discount = 12, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/sony-wh1000xm4.jpg", UrlSlug = "sony-wh1000xm4" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[3].Id, CategoryId = categories[8].Id, Name = "Dell Alienware", Description = "Top-tier gaming desktop with cutting-edge hardware.", Discount = 18, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/alienware.jpg", UrlSlug = "dell-alienware" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[4].Id, CategoryId = categories[1].Id, Name = "HP Envy 15", Description = "Sleek laptop with powerful Intel i7 processor.", Discount = 12, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/hp-envy15.jpg", UrlSlug = "hp-envy15" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[5].Id, CategoryId = categories[2].Id, Name = "Lenovo Yoga Smart Tab", Description = "Versatile tablet with built-in kickstand.", Discount = 7, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/lenovo-yoga.jpg", UrlSlug = "lenovo-yoga-smart-tab" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[6].Id, CategoryId = categories[0].Id, Name = "Microsoft Surface Duo", Description = "Dual-screen device for productivity on the go.", Discount = 10, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/surface-duo.jpg", UrlSlug = "microsoft-surface-duo" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[7].Id, CategoryId = categories[3].Id, Name = "Asus ZenWatch 3", Description = "Stylish smartwatch with customizable faces.", Discount = 15, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/asus-zenwatch3.jpg", UrlSlug = "asus-zenwatch3" },
				new Product { Id = Guid.NewGuid(), BrandId = brands[8].Id, CategoryId = categories[7].Id, Name = "Acer Spin 5", Description = "Convertible laptop with a sleek design.", Discount = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Image = "https://example.com/images/acer-spin5.jpg", UrlSlug = "acer-spin5" }
			};

            var productHardwares = new List<ProductHardware>
            {
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[0].Id, Name = "256GB Storage", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[0].Id, Name = "64GB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[1].Id, Name = "128GB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[1].Id, Name = "256GB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[2].Id, Name = "128GB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[2].Id, Name = "256GB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[3].Id, Name = "Bluetooth Version",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[3].Id, Name = "LTE Version",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[4].Id, Name = "4K UHD",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[5].Id, Name = "Intel i7 Processor",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[5].Id, Name = "16GB RAM",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[6].Id, Name = "AMD Ryzen 5 Processor",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[6].Id, Name = "8GB RAM" , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[7].Id, Name = "NVIDIA RTX 3060",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[7].Id, Name = "16GB RAM",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[8].Id, Name = "1TB Storage",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware { Id = Guid.NewGuid(), ProductId = products[9].Id, Name = "AMD Ryzen 9 Processor",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[9].Id, Name = "32GB RAM", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[10].Id, Name = "Intel i9 Processor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[11].Id, Name = "256GB Storage", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[12].Id, Name = "Wireless Charging Case",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[13].Id, Name = "Active Noise Cancellation", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[14].Id, Name = "Industry-leading Noise Cancellation", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[15].Id, Name = "Intel i9 Processor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[15].Id, Name = "32GB RAM", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[16].Id, Name = "Intel i7 Processor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[17].Id, Name = "128GB Storage", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[18].Id, Name = "128GB Storage", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[19].Id, Name = "Customizable Watch Faces", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new ProductHardware {Id = Guid.NewGuid(), ProductId = products[20].Id, Name = "Convertible Design", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now}
            };

            var productImages = new List<ProductImage>
            {
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[0].Id, Type = 1, UrlImage = "https://example.com/images/iphone13-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[0].Id, Type = 2, UrlImage = "https://example.com/images/iphone13-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[1].Id, Type = 1, UrlImage = "https://example.com/images/galaxy-s21-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[1].Id, Type = 2, UrlImage = "https://example.com/images/galaxy-s21-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[2].Id, Type = 1, UrlImage = "https://example.com/images/ipad-pro-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[2].Id, Type = 2, UrlImage = "https://example.com/images/ipad-pro-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[3].Id, Type = 1, UrlImage = "https://example.com/images/galaxy-watch-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[3].Id, Type = 2, UrlImage = "https://example.com/images/galaxy-watch-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[4].Id, Type = 1, UrlImage = "https://example.com/images/sony-bravia-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[4].Id, Type = 2, UrlImage = "https://example.com/images/sony-bravia-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[5].Id, Type = 1, UrlImage = "https://example.com/images/dell-xps13-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[5].Id, Type = 2, UrlImage = "https://example.com/images/dell-xps13-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[6].Id, Type = 1, UrlImage = "https://example.com/images/hp-pavilion-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[6].Id, Type = 2, UrlImage = "https://example.com/images/hp-pavilion-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[7].Id, Type = 1, UrlImage = "https://example.com/images/lenovo-legion5-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[7].Id, Type = 2, UrlImage = "https://example.com/images/lenovo-legion5-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[8].Id, Type = 1, UrlImage = "https://example.com/images/xbox-series-x-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[8].Id, Type = 2, UrlImage = "https://example.com/images/xbox-series-x-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[9].Id, Type = 1, UrlImage = "https://example.com/images/asus-rog-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[9].Id, Type = 2, UrlImage = "https://example.com/images/asus-rog-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[10].Id, Type = 1, UrlImage = "https://example.com/images/acer-predator-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[10].Id, Type = 2, UrlImage = "https://example.com/images/acer-predator-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[11].Id, Type = 1, UrlImage = "https://example.com/images/huawei-mate40-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[11].Id, Type = 2, UrlImage = "https://example.com/images/huawei-mate40-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[12].Id, Type = 1, UrlImage = "https://example.com/images/airpods-pro-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[12].Id, Type = 2, UrlImage = "https://example.com/images/airpods-pro-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[13].Id, Type = 1, UrlImage = "https://example.com/images/galaxy-buds-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[13].Id, Type = 2, UrlImage = "https://example.com/images/galaxy-buds-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[14].Id, Type = 1, UrlImage = "https://example.com/images/sony-wh1000xm4-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[14].Id, Type = 2, UrlImage = "https://example.com/images/sony-wh1000xm4-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[15].Id, Type = 1, UrlImage = "https://example.com/images/alienware-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[15].Id, Type = 2, UrlImage = "https://example.com/images/alienware-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[16].Id, Type = 1, UrlImage = "https://example.com/images/hp-envy15-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[16].Id, Type = 2, UrlImage = "https://example.com/images/hp-envy15-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[17].Id, Type = 1, UrlImage = "https://example.com/images/razer-blade15-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[17].Id, Type = 2, UrlImage = "https://example.com/images/razer-blade15-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[18].Id, Type = 1, UrlImage = "https://example.com/images/apple-tv4k-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[18].Id, Type = 2, UrlImage = "https://example.com/images/apple-tv4k-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Content
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[19].Id, Type = 1, UrlImage = "https://example.com/images/xbox-series-s-slide1.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Slide
                new ProductImage { Id = Guid.NewGuid(), ProductId = products[19].Id, Type = 2, UrlImage = "https://example.com/images/xbox-series-s-content.jpg",  CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now } // Content
            };


            var productColors = new List<ProductColor>
            {
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[0].Id, RGB = "#000000", Price = 999.99, Quantity = 50, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[0].Id, RGB = "#FFFFFF", Price = 999.99, Quantity = 40, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[0].Id, RGB = "#FF0000", Price = 999.99, Quantity = 30, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Red

                // Colors for iPhone 13 - 64GB RAM
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[1].Id, RGB = "#000000", Price = 799.99, Quantity = 60, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[1].Id, RGB = "#FFFFFF", Price = 799.99, Quantity = 50, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[1].Id, RGB = "#FF0000", Price = 799.99, Quantity = 45, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Red

                // Colors for Samsung Galaxy S21 - 128GB RAM
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[2].Id, RGB = "#6D6E71", Price = 849.99, Quantity = 70, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom Gray
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[2].Id, RGB = "#F8F8F8", Price = 849.99, Quantity = 65, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[2].Id, RGB = "#8C4B9A", Price = 849.99, Quantity = 60, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom Violet

                // Colors for Samsung Galaxy S21 - 256GB RAM
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[3].Id, RGB = "#6D6E71", Price = 999.99, Quantity = 55, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom Gray
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[3].Id, RGB = "#F8F8F8", Price = 999.99, Quantity = 50, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[3].Id, RGB = "#8C4B9A", Price = 999.99, Quantity = 45, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Phantom Violet

                // Colors for iPad Pro - 128GB
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[4].Id, RGB = "#3C3C3C", Price = 799.99, Quantity = 85, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Space Gray
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[4].Id, RGB = "#C0C0C0", Price = 799.99, Quantity = 80, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Silver

                // Colors for iPad Pro - 256GB
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[5].Id, RGB = "#3C3C3C", Price = 999.99, Quantity = 75, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Space Gray
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[5].Id, RGB = "#C0C0C0", Price = 999.99, Quantity = 70, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Silver

                // Colors for Dell XPS 13 - 16GB RAM
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[6].Id, RGB = "#C0C0C0", Price = 1199.99, Quantity = 30, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Platinum Silver
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[6].Id, RGB = "#F7F7F7", Price = 1199.99, Quantity = 25, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Frost White

                // Colors for Dell XPS 13 - 32GB RAM
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[7].Id, RGB = "#C0C0C0", Price = 1399.99, Quantity = 35, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Platinum Silver
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[7].Id, RGB = "#F7F7F7", Price = 1399.99, Quantity = 30, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Frost White

                // Colors for Sony Bravia 55-inch
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[8].Id, RGB = "#000000", Price = 1299.99, Quantity = 40, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[8].Id, RGB = "#FFFFFF", Price = 1299.99, Quantity = 35, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[8].Id, RGB = "#CCCCCC", Price = 1299.99, Quantity = 30, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Gray

                // Colors for HP Pavilion Desktop
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[9].Id, RGB = "#8B8B8B", Price = 499.99, Quantity = 50, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Gray
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[9].Id, RGB = "#FFFFFF", Price = 499.99, Quantity = 45, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[9].Id, RGB = "#000000", Price = 499.99, Quantity = 40, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black

                // Colors for Lenovo Legion 5
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[10].Id, RGB = "#000000", Price = 1199.99, Quantity = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[10].Id, RGB = "#4B4B4B", Price = 1199.99, Quantity = 18, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Dark Gray

                // Colors for Microsoft Xbox Series X
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[11].Id, RGB = "#000000", Price = 499.99, Quantity = 25, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[11].Id, RGB = "#4F4F4F", Price = 499.99, Quantity = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Dark Gray

                // Colors for Asus ROG Zephyrus
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[12].Id, RGB = "#000000", Price = 1499.99, Quantity = 15, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Black
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[12].Id, RGB = "#343434", Price = 1499.99, Quantity = 12, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Dark Gray

                // Colors for Google Pixel 6
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[13].Id, RGB = "#FFFFFF", Price = 1199.99, Quantity = 10, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // White
                new ProductColor { Id = Guid.NewGuid(), ProductHardwareId = productHardwares[13].Id, RGB = "#7B7B7B", Price = 1199.99, Quantity = 8, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }  // Dark Gray
            };


            var invoices = new List<Invoice>
			{
				new Invoice
				{
					Id = Guid.NewGuid(),
					CustomerId = customers[0].ID,
					Status = 1, // Prepare
                    MethodPaymment = "Credit Card",
					CreatedAt = DateTime.Now,
					Address = "New York, USA",
					Name= "John Doe",
                    Telephone = "123456789",
					CollaboratorId = collaborators[0].ID
                },
				new Invoice
				{
					Id = Guid.NewGuid(),
					CustomerId = customers[0].ID,
					Status = 2, // Delivering
                    MethodPaymment = "PayPal",
					CreatedAt = DateTime.Now.AddDays(-5),
					Address = "Los Angeles, USA",
                    Name= "Jane Doe",
                    Telephone = "987654321",
                    CollaboratorId = collaborators[0].ID
                },
				new Invoice
				{
					Id = Guid.NewGuid(),
					CustomerId = customers[0].ID,
					Status = 3, // Complete
                    MethodPaymment = "Bank Transfer",
					CreatedAt = DateTime.Now.AddDays(-10),
                    Address = "San Francisco, USA",
                    Name= "John Doe",
                    Telephone = "123456789",
                    CollaboratorId = collaborators[0].ID
                }
			};

			var invoiceProducts = new List<InvoiceProductColors>
			{
				new InvoiceProductColors { InvoiceId = invoices[0].Id, ProductColorsId = productColors[0].Id, Quantity = 1, ProductPrice = 999.99},
				new InvoiceProductColors { InvoiceId = invoices[0].Id, ProductColorsId = productColors[1].Id, Quantity = 2, ProductPrice = 799.99},
				new InvoiceProductColors { InvoiceId = invoices[1].Id, ProductColorsId = productColors[2].Id, Quantity = 1, ProductPrice = 849.99},
				new InvoiceProductColors { InvoiceId = invoices[1].Id, ProductColorsId = productColors[3].Id, Quantity = 3, ProductPrice = 999.99},
				new InvoiceProductColors { InvoiceId = invoices[2].Id, ProductColorsId = productColors[4].Id, Quantity = 1, ProductPrice = 1199.99},
				new InvoiceProductColors { InvoiceId = invoices[2].Id, ProductColorsId = productColors[5].Id, Quantity = 1, ProductPrice = 1199.99}
			};

            var specifications = new List<Specification>
			{
				new Specification { Id = Guid.NewGuid(), Name = "RAM", CategoryId = categories[0].Id },
				new Specification { Id = Guid.NewGuid(), Name = "Chip", CategoryId =  categories[0].Id },
    
				new Specification { Id = Guid.NewGuid(), Name = "RAM", CategoryId = categories[1].Id },
				new Specification { Id = Guid.NewGuid(), Name = "Storage", CategoryId = categories[1].Id },
    
				new Specification { Id = Guid.NewGuid(), Name = "Screen Size", CategoryId = categories[5].Id },
				new Specification { Id = Guid.NewGuid(), Name = "Resolution", CategoryId = categories[5].Id },
    
				new Specification { Id = Guid.NewGuid(), Name = "Graphic Card", CategoryId = categories[4].Id },
				new Specification { Id = Guid.NewGuid(), Name = "Processor", CategoryId = categories[4].Id },
			};

            var filters = new List<Filter>
            {
                new Filter { Id = Guid.NewGuid(), Name = "4GB RAM", SpecificationId = specifications[0].Id, CategoryId = categories[0].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "128GB Storage", SpecificationId = specifications[0].Id, CategoryId = categories[0].Id , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Filter { Id = Guid.NewGuid(), Name = "8GB RAM", SpecificationId = specifications[0].Id, CategoryId = categories[1].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "256GB Storage", SpecificationId = specifications[0].Id, CategoryId = categories[1].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "OLED Display", SpecificationId = specifications[0].Id, CategoryId = categories[2].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "5G Connectivity", SpecificationId = specifications[0].Id, CategoryId = categories[2].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "Water Resistant", SpecificationId = specifications[0].Id, CategoryId = categories[3].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "Heart Rate Monitor", SpecificationId = specifications[0].Id, CategoryId = categories[3].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Filter { Id = Guid.NewGuid(), Name = "4K UHD", SpecificationId = specifications[0].Id, CategoryId = categories[5].Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            };

            var productFilters = new List<ProductFilter>
            {
                new ProductFilter { ProductId = products[0].Id, FilterId = filters[0].Id },
                new ProductFilter { ProductId = products[1].Id, FilterId = filters[1].Id },
                new ProductFilter { ProductId = products[2].Id, FilterId = filters[2].Id },
                new ProductFilter { ProductId = products[3].Id, FilterId = filters[3].Id },
                new ProductFilter { ProductId = products[4].Id, FilterId = filters[4].Id },
                new ProductFilter { ProductId = products[5].Id, FilterId = filters[5].Id },
                new ProductFilter { ProductId = products[6].Id, FilterId = filters[6].Id },
                new ProductFilter { ProductId = products[7].Id, FilterId = filters[7].Id },
                new ProductFilter { ProductId = products[8].Id, FilterId = filters[8].Id },
                new ProductFilter { ProductId = products[10].Id, FilterId = filters[0].Id },
                new ProductFilter { ProductId = products[11].Id, FilterId = filters[1].Id },
                new ProductFilter { ProductId = products[12].Id, FilterId = filters[2].Id },
                new ProductFilter { ProductId = products[13].Id, FilterId = filters[3].Id },
                new ProductFilter { ProductId = products[14].Id, FilterId = filters[4].Id },
                new ProductFilter { ProductId = products[15].Id, FilterId = filters[5].Id },
                new ProductFilter { ProductId = products[16].Id, FilterId = filters[6].Id },
                new ProductFilter { ProductId = products[17].Id, FilterId = filters[7].Id },
                new ProductFilter { ProductId = products[18].Id, FilterId = filters[8].Id },
            };

            var orders = new List<Order>
			{
				new Order
				{
					CustomerId = customers[0].ID,
					ProductColorId = productColors[0].Id,
					Quanitity = 2,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new Order
				{
					CustomerId = customers[0].ID,
                    ProductColorId = productColors[1].Id,
					Quanitity = 1,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new Order
				{
					CustomerId = customers[0].ID,
                    ProductColorId = productColors[2].Id,
					Quanitity = 3,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new Order
				{
					CustomerId = customers[0].ID,
                    ProductColorId = productColors[3].Id,
					Quanitity = 1,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				},
				new Order
				{
					CustomerId = customers[0].ID,
                    ProductColorId = productColors[4].Id,
					Quanitity = 2,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				}
			};

			var comments = new List<Comment>
			{
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[0].Id,
					ProductId = products[0].Id,
					CommentText = "Good",
					Rate = 5,
					CreatedAt = DateTime.Now,
				},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[1].Id,
					ProductId = products[1].Id,
					CommentText = "Not good",
					Rate = 3,
					CreatedAt = DateTime.Now,
				},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[2].Id,
					ProductId = products[2].Id,
					CommentText = "Excellent value for money!",
					Rate = 5,
					CreatedAt = DateTime.Now,
				},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[3].Id,
					ProductId = products[3].Id,
					CommentText = "Not worth the price.",
					Rate = 2,
					CreatedAt = DateTime.Now,
				},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[0].Id,
					ProductId = products[4].Id,
					CommentText = "Great customer service.",
					Rate = 4,
					CreatedAt = DateTime.Now,
				},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[1].Id,
					ProductId = products[5].Id,
					CommentText = "The product arrived damaged.",
					Rate = 1,
					CreatedAt = DateTime.Now,
				}
			};

			modelBuilder.Entity<Comment>().HasData(comments);
			var primaryComments = comments.ToDictionary(c => c.Id);

			var replies = new List<Comment>
			{
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[1].Id,
					ProductId = products[0].Id,
					CommentText = "I agree with you!",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[0].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[2].Id,
					ProductId = products[0].Id,
					CommentText = "I had a different experience.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[0].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[0].Id,
					ProductId = products[1].Id,
					CommentText = "Why do you think so?",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[1].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[2].Id,
					ProductId = products[1].Id,
					CommentText = "I had a similar issue.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[1].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[3].Id,
					ProductId = products[2].Id,
					CommentText = "I agree, it's a great product.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[2].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[1].Id,
					ProductId = products[3].Id,
					CommentText = "I thought it was okay.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[3].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[0].Id,
					ProductId = products[3].Id,
					CommentText = "It depends on what you're looking for.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[3].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[2].Id,
					ProductId = products[4].Id,
					CommentText = "I had a different experience with the service.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[4].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[3].Id,
					ProductId = products[4].Id,
					CommentText = "They were very helpful for me too.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[4].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[0].Id,
					ProductId = products[5].Id,
					CommentText = "Did you contact support?",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[5].Id) // Set the ID of the primary comment this reply is for
            	},
				new Comment
				{
					Id = Guid.NewGuid(),
					UserId = users[3].Id,
					ProductId = products[5].Id,
					CommentText = "I had the same issue. They sent a replacement quickly.",
					Rate = 0,
					CreatedAt = DateTime.Now,
					UserReplyId = primaryComments.Keys.First(c => c == comments[5].Id) // Set the ID of the primary comment this reply is for
            	}
			};

			var chats = new List<Chat>()
			{
				new Chat
				{
					Id = Guid.NewGuid(),
					CustomerId = customers[0].ID,
					CollaboratorId = collaborators[0].ID,
					CreatedAt = DateTime.Now
				},
				new Chat
				{
                    Id = Guid.NewGuid(),
                    CustomerId = customers[0].ID,
                    CollaboratorId = collaborators[1].ID,
                    CreatedAt = DateTime.Now
                }
			};

			var messages = new List<Message>()
			{
				new Message
				{
					Id = Guid.NewGuid(),
					ChatId = chats[0].Id,
					Text_message = "Hello you",
					CreateAt = DateTime.Now,
					IsCustomer = true,
				},
				new Message
				{
                    Id = Guid.NewGuid(),
                    ChatId = chats[0].Id,
                    Text_message = "Hello, how are you today",
                    CreateAt = DateTime.Now.AddMinutes(1),
                    IsCustomer = false,
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    ChatId = chats[0].Id,
                    Text_message = "Im fine, but my boss at FPT 'Chen ep' me, i can't work at the company anymore",
                    CreateAt = DateTime.Now.AddMinutes(2),
                    IsCustomer = true,
                },
                 new Message
                {
                     Id = Guid.NewGuid(),
                    ChatId = chats[0].Id,
                    Text_message = "Your boss is Nguyen Phuc Du",
                    CreateAt = DateTime.Now.AddMinutes(3),
                    IsCustomer = false,
                },
                new Message
                {
					Id = Guid.NewGuid(),
                    ChatId = chats[0].Id,
                    Text_message = "Yes, i working like a dog day and day, and he not allow me rest, alse the holiday he assign so many task to me",
                    CreateAt = DateTime.Now.AddMinutes(4),
                    IsCustomer = true,
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    ChatId = chats[0].Id,
                    Text_message = "Im sorry about that, i hear so many the complain about your boss",
                    CreateAt = DateTime.Now.AddMinutes(5),
                    IsCustomer = false,
                },
				new Message
				{
                    Id = Guid.NewGuid(),
                    ChatId = chats[1].Id,
                    Text_message = "Hello",
                    CreateAt = DateTime.Now.AddMinutes(6),
                    IsCustomer = true,
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    ChatId = chats[1].Id,
                    Text_message = "F*ck you",
                    CreateAt = DateTime.Now.AddMinutes(7),
                    IsCustomer = false,
                },
            };


			modelBuilder.Entity<Comment>().HasData(replies);

			modelBuilder.Entity<User>().HasData(users);
			modelBuilder.Entity<Customer>().HasData(customers);
			modelBuilder.Entity<Admin>().HasData(admins);
			modelBuilder.Entity<Collaborator>().HasData(collaborators);
			modelBuilder.Entity<Category>().HasData(categories);
			modelBuilder.Entity<Brand>().HasData(brands);
			modelBuilder.Entity<Filter>().HasData(filters);
			modelBuilder.Entity<Product>().HasData(products);
			modelBuilder.Entity<ProductFilter>().HasData(productFilters);
			modelBuilder.Entity<Invoice>().HasData(invoices);
			modelBuilder.Entity<InvoiceProductColors>().HasData(invoiceProducts);
			modelBuilder.Entity<Order>().HasData(orders);
			modelBuilder.Entity<ProductHardware>().HasData(productHardwares);
			modelBuilder.Entity<ProductImage>().HasData(productImages);
			modelBuilder.Entity<ProductColor>().HasData(productColors);
            modelBuilder.Entity<Specification>().HasData(specifications);
			modelBuilder.Entity<Chat>().HasData(chats);
			modelBuilder.Entity<Message>().HasData(messages);
        }
    }
}
