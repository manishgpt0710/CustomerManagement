using CustomerManagement.Filters;
using System.Reflection;
using CustomerManagement.Middleware;
using CustomerManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerManagement.Data.Repository.Contract;
using CustomerManagement.Data.Repository.Service;
using CustomerManagement.Data.UOW;
using CustomerManagement.WebApi.BusinessLogic.Contracts;
using CustomerManagement.WebApi.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DatabaseConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CustomerManagement.WebApi"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(IContactInfoRepository), typeof(ContactInfoRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();

builder.Services.AddControllers();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger documentation configuration
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Customer Management API",
        Version = "v1",
        Description = "Customer Management System"
    });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    //var dtoXmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "DTO.xml");

    //... and tell Swagger to use those XML comments.
    //s.IncludeXmlComments(xmlPath);
    //s.IncludeXmlComments(dtoXmlPath);
    s.OperationFilter<SwaggerHeader>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseWhen(context => context.Request.Path.Value.StartsWith("/api"),
//     builder => { builder.UseExceptionHandler("/api/error/500"); });
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }