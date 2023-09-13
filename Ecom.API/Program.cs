using Ecom.API.Error;
using Ecom.API.MiddliWare;
using Ecom.Core.Interfaces;
using Ecom.infrastructure;
using Ecom.infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace Ecom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(op =>
                {
                    op.InvalidModelStateResponseFactory = context =>
                    {
                        var errorRespone = new APIValidationError
                        {
                            Error = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray(),
                        };
                        return new BadRequestObjectResult(errorRespone);
                    };
                });
                ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                var securty = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "jwt Auth Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme="Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                op.AddSecurityDefinition("Bearer", securty);
                var SR = new OpenApiSecurityRequirement { { securty, new[] { "Bearer" } } };
                op.AddSecurityRequirement(SR);
            });
            builder.Services.infarStrctureregistration(builder.Configuration);
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")
                
                
                ));
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                // x.RequireHttpsMetadata = false;
                // x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ve...@!.#ryv.][erysecret...@!.#2.][pws@]")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };
            });
            // confgure Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var confgur = ConfigurationOptions.Parse(builder.Configuration
                    .GetConnectionString("Redis"),true);
                return ConnectionMultiplexer.Connect(confgur);
            });
            builder.Services.AddCors(op =>
            {
                op.AddPolicy("CorsePolicy", pol =>
                {
                    pol.AllowAnyHeader();
                    pol.AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });
            builder.Services.AddScoped<ITokenService, TokenService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsePolicy");
            app.UseMiddleware<ExceptionMiddliWare>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            InfarStrctureRegistration.infraStructurConfigMidlleware(app);

            app.MapControllers();

            app.Run();
        }
    }
}