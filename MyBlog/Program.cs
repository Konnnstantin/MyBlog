
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlog.Domains.Entity;
using MyBlog.DAL;
using MyBlog.DAL.Interface;
using NLog.Web;
using MyBlog.Service.Interfaces;
using MyBlog.Service;

namespace OwlBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // Connect AutoMapper
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new Mapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            // Connect DataBase
            string? connection = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection))
                .AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();


            // Services AddSingletons/Transient
            builder.Services
                .AddSingleton(mapper)
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<ITagService, TagServices>()
                .AddTransient<IArticleService, ArticleService>()
                .AddTransient<ICommentService, CommentServices>()
                .AddTransient<IUnitOfwork, UnitOfWork>();


            // Connect logger
            builder.Logging
                .ClearProviders()
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole()
                .AddNLog("NLog");



            // Start WebApplication
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}