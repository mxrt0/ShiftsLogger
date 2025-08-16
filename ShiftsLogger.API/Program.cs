using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Services;
using ShiftsLogger.API.Services.Contracts;
using ShiftsLogger.Data.Context;

namespace ShiftsLogger.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<ShiftsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
        builder.Services.AddScoped<IShiftsService, ShiftsService>();

        builder.Services.AddScoped<IWorkerService, WorkerService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
