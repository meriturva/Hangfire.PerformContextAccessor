using Hangfire;
using Hangfire.PerformContextAccessor;
using Sample.Jobs;
using Sample.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfirePerformContextAccessor();

// Add Hangfire services.
GlobalConfiguration.Configuration.UseInMemoryStorage();
builder.Services.AddHangfire(config =>
{
    // Add filter to handle PerformContextAccessor
    config.UsePerformContextAccessorFilter();
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