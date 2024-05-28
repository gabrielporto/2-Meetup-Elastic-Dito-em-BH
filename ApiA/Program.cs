
using Quartz.Impl;
using Quartz.Spi;
using Quartz;

namespace ApiA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration["ElasticApm:ServiceName"] = builder.Environment.ApplicationName;
            builder.Configuration["ElasticApm:Environment"] = builder.Environment.EnvironmentName;
            builder.Configuration["ElasticApm:ServerUrls"] = "http://apm-server:8200";
            builder.Services.AddAllElasticApm();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Add Quartz services
            builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            builder.Services.AddSingleton<HelloJob>();
            builder.Services.AddSingleton(new JobSchedule(
                jobType: typeof(HelloJob),
                cronExpression: "0/40 * * * * ?")); // Execute every 40 seconds

            builder.Services.AddHostedService<QuartzHostedService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
