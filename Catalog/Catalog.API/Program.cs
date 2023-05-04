using Catalog.Business.Configuration.Settings;
using DependencyResolver;
using Microsoft.EntityFrameworkCore;
using ORM.Context;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add config options
builder.Services.Configure<RabbitMqServerSettings>(builder.Configuration.GetSection("RabbitMqServerSettings"));

// Add db to the container
builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureFilters();
builder.Services.ConfigureSettings();

//Add mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
