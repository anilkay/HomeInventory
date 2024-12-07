using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HomeInventory.Configuration;

public static  class AppConfiguration
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("HomeInventory"))
            .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(new AlwaysOnSampler())
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri("http://localhost:4317"); // gRPC
                        otlpOptions.Protocol = OtlpExportProtocol.Grpc;
                    });


            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel");
                //.AddConsoleExporter();
            });
           

        services.AddDbContext<HomeInventoryDbContext>(
            
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
    }
}