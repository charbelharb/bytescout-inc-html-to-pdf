using System.Text;
using FooterAppender;
using FooterAppender.Interfaces;
using HtmlConverter;
using HtmlConverter.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Interfaces;
using WebApi.Context;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DI
builder.Services.AddTransient<IParser, Parser>();
builder.Services.AddTransient<IFooter, Footer>();
builder.Services.AddTransient<IConverterService, ConverterServiceService>();

// Add authentication
builder.Services.AddDbContext<HtmlToPdfContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiContext")!);
});
builder.Services.AddIdentity<HtmlToPdfUser, IdentityRole>().AddEntityFrameworkStores<HtmlToPdfContext>();

// Configuration are not secure - this is just to be as simple as possible
builder.Services.AddIdentityCore<HtmlToPdfUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
}).AddDefaultTokenProviders();

new IdentityBuilder(typeof(HtmlToPdfUser), typeof(IdentityRole), builder.Services)
    .AddSignInManager<SignInManager<HtmlToPdfUser>>()
    .AddEntityFrameworkStores<HtmlToPdfContext>();

// Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy  =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HtmlToPdfContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();