
using Microsoft.EntityFrameworkCore;
using StudenManagementBackEnd.Models;

namespace StudenManagementBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var myAllowSpecificOrigins = "myAllowSpecificOrigins"; // why ?

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddDbContext<StudenContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Myconnection"));

            });

            // Enable Cors  why

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            //
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           


            app.UseHttpsRedirection();

            app.UseCors(myAllowSpecificOrigins); // why?

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
