using Hegymegi_Kiss_Ákos_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Hegymegi_Kiss_Ákos_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ============================================
            // UID tárolása a főprogramban (feladat 13)
            // ============================================
            string UID = "FKB3F4FEA09CE43C";
            builder.Configuration["UID"] = UID;

            // ============================================
            // Controllers + körkörös hivatkozás kezelése (feladat 8)
            // ============================================
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // ============================================
            // DbContext regisztrálása
            // ============================================
            builder.Services.AddDbContext<LibrarydbContext>(option =>
            {
                var conn = builder.Configuration.GetConnectionString("MySql");
                option.UseMySQL(conn);
            });

            // ============================================
            // CORS beállítása (feladat 14)
            // ============================================
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
