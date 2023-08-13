using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Application;
using NetCoreCleanArchitecture.Infrastructure;
using NetCoreCleanArchitecture.Persistence;

namespace NetCoreCleanArchitecture.Api;
public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen();
        }

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("Open");
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetService<NetCoreCleanArchitectureDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Write(ex.Message, ex.StackTrace);
        }
    }
}