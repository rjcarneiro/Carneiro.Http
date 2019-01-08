# Carneiro Http

Lightweight library to do Http requests easily. 

## How to use

```csharp
var options = Options.Create<HttpOrchestratorOptions>(new HttpOrchestratorOptions
{
    Url = "http://myurl.com/api"
});

var httpOrchestrator = new HttpOrchestrator(options);

List<T> myCollection = await httpOrchestrator.GetAsync<T>("/users");
```

## Changelogs

### [1.0.1] - 2019-01-08

 - Added Xml comments;
 - Add unit tests;

### [1.0.0] - 2019-01-07

 - First release of the library;

## Team

@rjcarneiro