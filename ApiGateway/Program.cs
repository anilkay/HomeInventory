using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var additionalEnvironmentInformation = Environment.GetEnvironmentVariable("ADDITIONAL_INFO");

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.{additionalEnvironmentInformation}.json", optional: true, reloadOnChange: true)

        //ADDITIONAL_INFO
        .AddEnvironmentVariables();
});


builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

app.Run();