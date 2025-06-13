using CollageAppp.MyLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddLog4Net();

#region serilog setting
//Log.Logger = new LoggerConfiguration().
//    MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt",rollingInterval: RollingInterval.Minute)
//    .CreateLogger();
////use the line to override
//builder.Host.UseSerilog();

////use serilog along with built in loggers
//builder.Logging.AddSerilog();
#endregion

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyLogger, LogToFile>();
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
