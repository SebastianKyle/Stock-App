using Serilog;
using StocksApp.UI.Middlewares;
using StocksApp.UI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {
  loggerConfiguration.ReadFrom.Configuration(context.Configuration) // read configuration setting from built-in IConfiguration
                     .ReadFrom.Services(services); // read the services of our current app and make them avaiable to serilog
});

if (!builder.Environment.IsDevelopment())
{
  builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Enables the endpoint completion log
app.UseSerilogRequestLogging();

if (builder.Environment.IsDevelopment()) 
{
  app.UseDeveloperExceptionPage();
}
else 
{
  app.UseExceptionHandler("/Error");
  app.UseExceptionHandlingMiddleware();
}

app.UseHttpLogging();

if (builder.Environment.IsEnvironment("Test") == false)
{
  Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }
