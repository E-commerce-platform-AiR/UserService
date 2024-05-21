using Microsoft.EntityFrameworkCore;
using UserService.Database.DbContext;
using UserService.Database.Repositories;
using UserService.Database.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureControllers(services);
        ConfigureSwagger(services);
        ConfigureScopedServices(services);
        ConfigureDatabaseContext(services);
    }
    
    private static void ConfigureControllers(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddEndpointsApiExplorer();
    }

    private void ConfigureDatabaseContext(IServiceCollection services)
    {
        services.AddDbContext<ShopDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("ShopDbContext"));
        });
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
    private static void ConfigureScopedServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, Services.UserService>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseWebSockets();
        app.UseCors(policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
            endpoint.MapRazorPages();
        });
    }
    
}