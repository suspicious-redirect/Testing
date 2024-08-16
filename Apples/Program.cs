using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Apple.Data;
using Apple.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, AppleAuthHandler>("AppleAuthentication", null);
builder.Services.AddDbContext<AppleDBContext>(options => options.UseSqlite(builder.Configuration["A2Connection"] ?? "Connection Error"));
builder.Services.AddScoped<IAppleRepo, AppleRepo>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnly", policy => policy.RequireClaim("userName"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
