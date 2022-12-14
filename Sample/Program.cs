using Hangfire;
using Hangfire.PerformContextAccessor;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sample.Jobs;
using Sample.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfirePerformContextAccessor();

// Add Hangfire services.
GlobalConfiguration.Configuration.UseInMemoryStorage();
builder.Services.AddHangfire((serviceProvider, config) =>
{
    // Add filter to handle PerformContextAccessor
    config.UsePerformContextAccessorFilter(serviceProvider);
});

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
builder.Services.AddTransient<ITestService, TestService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

BackgroundJob.Enqueue<SimpleJob>(sj => sj.DoJobAsync());

app.Run();