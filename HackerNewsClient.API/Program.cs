using Books.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Throttle the thread pool (set available threads to amount of processors)
ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IHackerNewsRepository, HackerNewsRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.H

app.UseAuthorization();

app.MapControllers();

app.Run();
