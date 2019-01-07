# Carneiro Http

Lightweight library to do Http requests easily. 

## How to use

```csharp
var httpOrchestrator = new HttpOrchestrator(new HttpOrchestratorOptions{
    Url = "http://myurl.com/api"
});

List<T> myCollection = await httpOrchestrator.GetAsync<T>("/users");

```
