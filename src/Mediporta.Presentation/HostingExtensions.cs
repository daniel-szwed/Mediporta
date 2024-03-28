using MediatR;
using Mediporta.Application.Commands;
using Mediporta.Application.Models;
using Mediporta.Domain;
using Mediporta.Domain.Repositories;
using Mediporta.Domain.Services;
using Mediporta.Infrastructure;
using Mediporta.Infrastructure.Repositories;
using Mediporta.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Presentation;

public static class HostingExtensions
{
    public static WebApplicationBuilder SetupOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TagSource>(
            builder.Configuration.GetSection(nameof(TagSource)));
        
        return builder;
    }
    
    public static IServiceCollection DependencySetup(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlite("Filename=Mediporta.db"));
        services.AddTransient<IRepository<Tag>, Repository<Tag>>();
        services.AddTransient<IApiService, ApiService>();
        services.AddTransient<ITagService, TagService>();
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        
        return services;
    }
    
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();
        }

        return app;
    }
    
    public static WebApplication FetchTags(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var mediator = serviceScope.ServiceProvider.GetService<IMediator>();
            var request = new RecreateDatabaseCommand();
            mediator.Send(request).GetAwaiter().GetResult();
        }

        return app;
    }
}