using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Products.Data;
using Products.Model;
using System.Text;

namespace Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectinString = builder.Configuration.GetConnectionString("MyDB");
            // Add DbContext
            builder.Services.AddDbContext<MyDbContext>(options => options.UseMySQL(connectinString));
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            var secertKey = builder.Configuration["Appsettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secertKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(otp =>
                {
                    otp.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Tu cap token
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //Ky vao token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero
                    };
                });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
       
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
        }
    }
}