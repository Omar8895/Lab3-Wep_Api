using Lab3.Data.Context;
using Lab3.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Configurations
var connectionString = builder.Configuration.GetConnectionString("Company");
builder.Services.AddDbContext<CompanyContext>(options =>
    options.UseSqlServer(connectionString));

//Identity Configurations
builder.Services.AddIdentity<User,IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase= false;
    options.Password.RequireDigit= false;
    options.User.RequireUniqueEmail= false;

}).AddEntityFrameworkStores<CompanyContext>();


//Authentication Configurations
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = "cool";
    Options.DefaultChallengeScheme = "cool";
}).AddJwtBearer("cool", options =>
{
string keystring = builder.Configuration.GetValue<string>("SecreteKey");
var KeyInBytes = Encoding.ASCII.GetBytes(keystring);
var key = new SymmetricSecurityKey(KeyInBytes);

    options.TokenValidationParameters = new TokenValidationParameters { 

        IssuerSigningKey = key,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Authorization Configurations 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy =>

        policy.RequireClaim(ClaimTypes.Role, "Manager", "Admin")
        .RequireClaim(ClaimTypes.NameIdentifier));

});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
    policy.RequireClaim(ClaimTypes.Role, "User", "Admin")
    .RequireClaim(ClaimTypes.NameIdentifier));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
