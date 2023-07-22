using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowLocalHost4200",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Add your front-end URL here
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Retrieve the SQL connection string from Azure Key Vault
        var keyVaultUrl = new Uri(Configuration.GetSection("keyVaultConfig:KVUrl").Value!);
        var keyVaultTenantId = Configuration.GetSection("keyVaultConfig:TenantId").Value;
        var keyVaultClientId = Configuration.GetSection("keyVaultConfig:ClientId").Value;
        var keyVaultSecret = Configuration.GetSection("keyVaultConfig:ClientSecretId").Value;

        var azureCredential = new ClientSecretCredential(keyVaultTenantId, keyVaultClientId, keyVaultSecret);

        var client = new SecretClient(keyVaultUrl, azureCredential);

        var connectionString = client.GetSecret(Configuration.GetSection("ConnectionStrings:Blog-DB").Value).Value.Value;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowLocalHost4200");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
