using Catalog.API.Middlewares;
using Catalog.Business.Configuration.Settings;
using DependencyResolver;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ORM.Context;
using System.Security.Claims;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:11111/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "swagger-API", "Swagger API" }
                },
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme,
                },
                Scheme = "oauth2",
                Name = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
}).AddIdentityServerAuthentication(options =>
{
    options.ApiName = "swagger-API";
    options.Authority = "https://localhost:11111";
    options.RequireHttpsMetadata = false;
    options.LegacyAudienceValidation = false;
    options.SaveToken = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Editors", builder =>
    {
        builder.RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, new string[] { "Manager" });
    });
    options.AddPolicy("Viewers", builder =>
    {
        builder.RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, new string[] { "Buyer", "Manager" });
    });
});

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

app.UseCorrelationIdMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("swagger-api-client-id");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
