# Hangfire.PerformContextAccessor

PerformContextAccessor make possible to access to PerformContext outside Job execution methods.

Installation
-------------

Hangfire is available as a NuGet package. You can install it using the NuGet Package Console window:

```
PM> Install-Package Bonura.Hangfire.PerformContextAccessor
```

After installation, update your existing [OWIN Startup](http://www.asp.net/aspnet/overview/owin-and-katana/owin-startup-class-detection) file with the following lines of code. If you do not have this class in your project or don't know what is it, please read the [Quick start](http://docs.hangfire.io/en/latest/quick-start.html) guide to learn about how to install Hangfire.

```csharp
public void Configuration(IAppBuilder app)
{
    // Register IPerformContextAccessor as transient service
    app.AddHangfirePerformContextAccessor();

    // Add Hangfire services.
    app.AddHangfire(config =>
    {
      // Add filter to handle PerformContextAccessor
      config.UsePerformContextAccessorFilter();
    });
}
```

Remarks
-------------
As already defined for IHttpContextAccessor

* IPerformContextAccessor interface should be used with caution. As always, the PerformContext must not be captured outside of the Job execution flow.
IPerformContextAccessor:
* Relies on System.Threading.AsyncLocal which can have a negative performance impact on asynchronous calls.
* Creates a dependency on "ambient state" which can make testing more difficult.
* IPerformContextAccessor.HttpContext may be null if accessed outside of the request flow.
* To access information from PerformContext outside the Job execution flow, copy the information inside the execution flow. Be careful to copy the actual data and not just references. For example, rather than copying a reference to an IDictionary, copy the relevant header values or copy the entire dictionary key by key before leaving the flow.
* Don't capture IPerformContextAccessor.PerformContext in a constructor.
