using AuthAPI.Services;

namespace AuthAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // opens the main method body
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Swagger/OpenAPI support
            builder.Services.AddEndpointsApiExplorer();   // Required for Swagger
            builder.Services.AddSwaggerGen();             // Adds Swagger generator

            builder.Services.AddScoped<IAuthService, AuthService>();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthAPI v1");
                });
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
