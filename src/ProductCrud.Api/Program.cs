using Microsoft.AspNetCore.Builder;
using ProductCrud.Api;
using ProductCrud.Api.Infrastructure;
using ProductCrud.Application;
using ProductCrud.Infrastructure;
using ProductCrud.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    ;

builder.AddWebServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UserApiServices();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseExceptionHandler(options => { });

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(); // Serves the OpenAPI/Swagger documents
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

app.Map("/", () => Results.Redirect("/api"));
app.UseAuthorization();
app.MapEndpoints();

app.Run();