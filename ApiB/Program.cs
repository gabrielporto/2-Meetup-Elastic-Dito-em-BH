
namespace ApiB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration["ElasticApm:ServiceName"] = builder.Environment.ApplicationName;
            builder.Configuration["ElasticApm:Environment"] = builder.Environment.EnvironmentName;
            builder.Configuration["ElasticApm:ServerUrls"] = "http://localhost:8200";
            builder.Services.AddAllElasticApm();

            // Add services to the container.

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
