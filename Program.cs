using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServeBook_Backend.Aplications.Services.Token;
using ServeBook_Backend.Data;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Aplications.Services;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ServeBooksContext> (options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

/* Interfaz */
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenServices, TokenServices>();


/* Configuracion del token */
builder.Services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(configure => {
            configure.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        /* Control de errores del token */
            configure.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        Console.WriteLine("Token expirado, Porfavor genere uno nuevo.");
                    }
                    else
                    {
                        Console.WriteLine("Usuario no autorizado.");
                    }
                    return Task.CompletedTask;
                }
            };
        });

/* EMAIL */
builder.Services.Configure<Email>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/* Configuracion de authentication y authorization */
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

