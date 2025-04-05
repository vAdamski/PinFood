using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(o =>
	{
		o.RequireHttpsMetadata = false;
		o.TokenValidationParameters = new TokenValidationParameters()
		{
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ClockSkew = TimeSpan.Zero,
		};
	});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();