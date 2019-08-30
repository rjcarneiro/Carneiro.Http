# Carneiro Http

![GitHub](https://img.shields.io/github/license/rjcarneiro/Carneiro.Http?style=flat-square) ![Nuget](https://img.shields.io/nuget/v/Carneiro.Host?style=flat-square) ![Nuget](https://img.shields.io/nuget/dt/Carneiro.Host?style=flat-square)


## Nuget Package

You can download this package directly from [Nuget.org](https://www.nuget.org/packages/Carneiro.Http).

## How to use

Register `HttpOrchestrator` with your `IServiceCollection` from your dotnet core project.

```csharp
services.RegisterHttpOrchestrator("http://myurl.com/api");
```

Then you can receive `HttpOrchestrator` on your ctor in your services or controllers. It's register as `transient`.

```csharp
public class AccountController
{
    private readonly HttpOrchestrator _httpOrchestrator;

    public AccountController(HttpOrchestrator httpOrchestrator)
    {
        _httpOrchestrator = httpOrchestrator;
    }

    public async Task<IActionResult> Index()
    {
        User user = await httpOrchestrator.GetAsync<User>("/users/1");
    }
}
```

In version 2.0.0 `HttpOrchestrator` works directly with `IHttpClientFactory` that, on the moment of `RegisterHttpOrchestrator` registers a http client named `HttpOrchestrator`.

## Changelogs

### [2.0.0] - 2019-08-16

- Drop `HttpOrchestratorOptions`;
- Drop disposable;
- Added `IHttpClientFactory` to manage http client life cycle.

### [1.1.0] - 2019-02-04

- Drop old dependencies;

### [1.0.2] - 2019-01-09

- Add IServiceCollection extensions;

### [1.0.1] - 2019-01-08

- Added Xml comments;
- Add unit tests;

### [1.0.0] - 2019-01-07

- First release of the library;

## Team

[Ricardo Carneiro](https://github.com/rjcarneiro/)