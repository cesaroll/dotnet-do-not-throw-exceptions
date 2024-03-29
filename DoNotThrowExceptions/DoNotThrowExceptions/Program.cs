using DoNotThrowExceptions.Services;
using Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
// app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();