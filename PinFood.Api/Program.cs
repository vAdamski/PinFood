using PinFood.Api.Configurations;
using PinFood.Application;
using PinFood.Infrastructure;
using PinFood.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.ConfigureAppSettings();
builder.ConfigureSerilog(builder.Configuration);
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();