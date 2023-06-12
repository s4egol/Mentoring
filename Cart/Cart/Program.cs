// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.Security.Claims;
using Cart.Business.Configuration.Settings;
using Cart.Configuration;
using Cart.Middlewares;
using DependencyResolver;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new NullReferenceException(nameof(connectionString));

// Add config options
builder.Services.Configure<RabbitMqServerSettings>(builder.Configuration.GetSection("RabbitMqServerSettings"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureSettings();
builder.Services.ConfigureDatabase(connectionString);
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

// Swagger options
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddScoped<IConfigureNamedOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//Add mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("swagger-api-client-id");

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAccessTokenMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
