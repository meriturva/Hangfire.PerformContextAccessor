# Hangfire.PerformContextAccessor

[![Latest version](https://img.shields.io/nuget/v/Bonura.Hangfire.PerformContextAccessor.svg)](https://www.nuget.org/packages?q=Bonura.Hangfire.PerformContextAccessor)

Use PerformContextAccessor to access to PerformContext outside Job execution methods. It's only necessary to use IPerformContextAccessor when you need access to the job PerformContext inside a service.

Installation
-------------

Hangfire.PerformContextAccessor is available as a NuGet package. You can install it using the NuGet Package Console window:

```
PM> Install-Package Bonura.Hangfire.PerformContextAccessor
```

After installation, update your existing [OWIN Startup](http://www.asp.net/aspnet/overview/owin-and-katana/owin-startup-class-detection) file with the following lines of code. If you do not have this class in your project or don't know what is it, please read the [Quick start](http://docs.hangfire.io/en/latest/quick-start.html) guide to learn about how to install Hangfire.

```csharp
public void Configuration(IAppBuilder app)
{
    // Register IPerformContextAccessor as transient
    app.AddHangfirePerformContextAccessor();

    // Add Hangfire service
    app.AddHangfire(config =>
    {
      // Add filter to handle PerformContextAccessor
      config.UsePerformContextAccessorFilter();
    });
}
```

Use
-------------
A simple job that just injects a service and uses it.

```csharp
using Sample.Service;

public class SimpleJob
{
    private ITestService _testService;

    public SimpleJob(ITestService testService)
    {
        _testService = testService;
    }

    public async Task DoJobAsync()
    {
        var jobId = await _testService.GetCurrentJobIdAsync();
    }
}
```

A service that use IPerformContextAccessor
```csharp
public class TestService : ITestService
{
    private IPerformContextAccessor _performContextAccessor;

    public TestService(IPerformContextAccessor performContextAccessor)
    {
        _performContextAccessor = performContextAccessor;
    }

    public async Task<string> GetCurrentJobIdAsync()
    {
        var jobId = _performContextAccessor.PerformingContext.BackgroundJob.Id;
        return await Task.FromResult(jobId);
    }
}
```

Motivation
-------------
Access PerformContext (basically to get JobId) on a custom NLog layout (`hangfire-jobid`):

`"layout": "${longdate}|${level:uppercase=true}|${logger}|${message}|${hangfire-jobid}|${exception:format=toString}",`

See *NLog Hangfire Layouts*: https://github.com/meriturva/NLog.HangfireLayouts

Remarks
-------------
As already defined for IHttpContextAccessor

* IPerformContextAccessor interface should be used with caution. As always, the PerformContext must not be captured outside of the Job execution flow.
* IPerformContextAccessor: Relies on System.Threading.AsyncLocal which can have a negative performance impact on asynchronous calls.
* Creates a dependency on "ambient state" which can make testing more difficult.
* IPerformContextAccessor.PerformContext may be null if accessed outside of the Job execution flow.
* To access information from PerformContext outside the Job execution flow, copy the information inside the execution flow. Be careful to copy the actual data and not just references. For example, rather than copying a reference to an IDictionary, copy the relevant header values or copy the entire dictionary key by key before leaving the flow.
* Don't capture IPerformContextAccessor.PerformContext in a constructor.
