using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBlog.DAL;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Service.Interfaces;
using MyBlog.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
             .AddCookie("Cookies", options =>
             {
                 options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                 {
                     OnRedirectToLogin = redirectContext =>
                     {
                         redirectContext.HttpContext.Response.StatusCode = 401;
                         return Task.CompletedTask;
                     }
                 };
             });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();
