# Sander0542.CMLedController
![Last commit](https://img.shields.io/github/last-commit/Sander0542/Sander0542.CMLedController?style=for-the-badge)
![License](https://img.shields.io/github/license/Sander0542/Sander0542.CMLedController?style=for-the-badge)

A library for the CoolerMaster RGB LED Controller

## Packages

| Package | NuGet Latest | Downloads |
|---------|--------------|-----------|
| Sander0542.CMLedController | ![Current release](https://img.shields.io/nuget/v/Sander0542.CMLedController) | ![Downloads](https://img.shields.io/nuget/dt/Sander0542.CMLedController) |
| Sander0542.CMLedController.Abstractions | ![Current release](https://img.shields.io/nuget/v/Sander0542.CMLedController.Abstractions) | ![Downloads](https://img.shields.io/nuget/dt/Sander0542.CMLedController.Abstractions) |
| Sander0542.CMLedController.Extensions | ![Current release](https://img.shields.io/nuget/v/Sander0542.CMLedController.Extensions) | ![Downloads](https://img.shields.io/nuget/dt/Sander0542.CMLedController.Extensions) |
| Sander0542.CMLedController.HidLibrary | ![Current release](https://img.shields.io/nuget/v/Sander0542.CMLedController.HidLibrary) | ![Downloads](https://img.shields.io/nuget/dt/Sander0542.CMLedController.HidLibrary) |

## Implementations

There are currently two implementations of this library. The default library `Sander0542.CMLedController` is using `Device.Net` for its processing. The package `Sander0542.CMLedController.HidLibrary` implements the `HidLibrary` library for processing.

| Package | Implementation | GitHub |
|---------|----------------|--------|
| Sander0542.CMLedController | [Device.Net](https://www.nuget.org/packages/Device.Net) | [Device.Net](https://github.com/MelbourneDeveloper/Device.Net) |
| Sander0542.CMLedController.HidLibrary | [HidLibrary](https://www.nuget.org/packages/HidLibrary/) | [HidLibrary](https://github.com/mikeobrien/HidLibrary) |

## Usage

### Simple

```c#
public async Task SetColorRed()
{
    var provider = new LedControllerProvider();
    var devices = await provider.GetControllersAsync();
    
    foreach (var device in devices) {
        await device.SetStaticAsync(Color.Red);
    }
}
```

### Dependency Injection

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ILedControllerProvider, LedControllerProvider>();
}
```

## Contributors
![https://github.com/Sander0542/Sander0542.CMLedController/graphs/contributors](https://contrib.rocks/image?repo=Sander0542/Sander0542.CMLedController)