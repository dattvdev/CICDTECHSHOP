using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Repositories;
using CloudinaryDotNet;
using System.Net;
using dotenv.net;
using Microsoft.AspNetCore.Identity.UI.Services;
using TechShop.Core.Services;
using Net.payOS;
using TechShop.Hubs;


namespace TechShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
			IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

			PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
								configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
								configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddSingleton(payOS);
			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IFilterRepository, FilterRepository>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductFilterRepository, ProductFilterRepository>();
            builder.Services.AddScoped<IProductHardwareRepository, ProductHardwareRepository>();
            builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
            builder.Services.AddScoped<ITrackingRepository, TrackingRepository>();
            builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ISpecificationRepository, SpecificationRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            })
                .AddEntityFrameworkStores<TechshopDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddSingleton<IEmailSender, EmailSender>();

            builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("MailSetting"));

            //DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            Cloudinary cloudinary = new Cloudinary("cloudinary://377843956877849:ToDQR4i4uXt9O_ck2N7Wgn9g-oY@du2ke2ics");
            cloudinary.Api.Secure = true;

            builder.Services.AddSingleton(cloudinary);
            builder.Services.AddTransient<ExcelPackage>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            builder.Services.AddDbContext<TechshopDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TechShop"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("TechShop.Core");
                }); 
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        var requestPath = context.Request.Path;

                        if (requestPath.StartsWithSegments("/Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            context.RedirectUri = "/Admin/Login";
                        }
                        else
                        {
                            context.RedirectUri = "/Customer/Login";
                        }
                        context.Response.Redirect(context.RedirectUri);
                        return Task.CompletedTask;
                    }
                };

            });
             

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login/";
                options.LogoutPath = "/logout/";
                options.AccessDeniedPath = "/AccessDenied/";
            });


            builder.Services.AddSession();
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

			app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapHub<ChatHub>("/chatHub");

			app.Run();
        }
    }
}
