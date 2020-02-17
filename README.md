# Utapau

Utapau is a lightweight library with extension methods for built-in .Net Core dependency injection container. 

[![Stars](https://img.shields.io/github/stars/ingvar1995/Utapau.svg?style=for-the-badge)](https://github.com/ingvar1995/Utapaustargazers)
[![Open issues](https://img.shields.io/github/issues/ingvar1995/Utapau.svg?style=for-the-badge)](https://github.com/ingvar1995/Utapau/issues)
[![License](https://img.shields.io/dub/l/vibe-d.svg?style=for-the-badge)](https://raw.githubusercontent.com/ingvar1995/Utapau/master/LICENSE.md)
![Build Status](https://img.shields.io/travis/ingvar1995/Utapau?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/v/Utapau?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Utapau?style=for-the-badge)

# Features

### Named dependencies

```csharp
services
    .AddSingleton<IService, FirstService>(nameof(FirstService))  // register named dependency
    .AddSingleton<IService, SecondService>(nameof(SecondService))  // register named dependency
    .AddSingleton(sp => new ThirdService(
        sp.GetRequiredService<IService>(nameof(FirstService)),  // resolve named dependency
        sp.GetRequiredService<IService>(nameof(SecondService))  // resolve named dependency
    ));
```

### Func<T> and Lazy<T> support

```csharp
services
    .AddSingleton<Service>()
    .AddFactory<Service>()  // registers Func<Service>
    .AddLazy<Service>();  // registers Lazy<Service>
```
 
### Providers support

```csharp
public interface IProvider<TService>
{
    TService Instance { get; }
}
 
services
    .AddSingleton<Service>()
    .AddProvider<Service>();  // registers IProvider<Service>
```

### Dependencies registration tests

```csharp
services.ResolveAllServices();  // resolves all services registered in service collection instance
```
 

