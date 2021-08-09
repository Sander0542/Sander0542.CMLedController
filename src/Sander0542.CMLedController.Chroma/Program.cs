using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Chroma.Services;
using Sander0542.CMLedController.DeviceDotNet;

namespace Sander0542.CMLedController.Chroma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => {
                    services.AddSingleton<ILedControllerProvider, DeviceDotNetLedControllerProvider>();
                    services.AddHostedService<ChromaService>();
                });
    }
}
