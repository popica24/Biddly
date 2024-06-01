using Business.Contracts;
using Licenta.Hubs;
using Licenta.Services;
using Microsoft.OpenApi.Models;
using Services;
using Services.CacheService;
using Services.Configuration;
using Services.Context;
using Swashbuckle.AspNetCore.Filters;
using WebAPI.Common.Mappings;
using WebAPI.Configuration;
using WebAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithOrigins("http://localhost:5173"); // URL of your React app
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSignalR();
builder.Services.AddTransient<IConnectionString>(x => new ConnectionString(builder.Configuration.GetConnectionString("Default")!));
builder.Services.AddMappings();
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<BidHub>("/biddingHub");

app.Run();
