using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Logging;
using Voters.Core.Extensions;
using Voters.Core.Models.Config;
using Voters.Core.Services;
using Voters.Extensions;
using Voters.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

// Configure JSON options
builder.Services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.ConfigSerializerOptions();
});

// Config sources
var environmentName = builder.Environment.EnvironmentName;
var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();

// Services
builder.Services.Configure<AppConfig>(configuration.GetSection(AppConfig.SectionName));
builder.Services.AddScoped<IContextService, ContextService>();
builder.Services.AddCoreServices();
var app = builder.Build();

//app.UseExceptionHandler(builder => builder.HandleException(loggerFactory));
app.UseContextMiddleWare();
app.UseLoggingMiddleWare();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseEndpointExtensions();

app.Run();