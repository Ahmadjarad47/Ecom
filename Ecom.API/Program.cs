using Ecom.API.Error;
using Ecom.API.MiddliWare;
using Ecom.infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
using System.Reflection;

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
            builder.Services.AddSwaggerGen();
            builder.Services.infarStrctureregistration(builder.Configuration);
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")
                
                
                ));
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
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}