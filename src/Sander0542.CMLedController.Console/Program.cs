using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Extensions;
using Sander0542.CMLedController.HidLibrary;
using Sander0542.CMLedController.HidSharp;

namespace Sander0542.CMLedController.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationToken token = default;
            ILedControllerProvider provider;
            // provider = new LedControllerProvider();
            // provider = new HidLibraryLedControllerProvider();
            provider = new HidSharpLedControllerProvider();

            var devices = await provider.GetControllersAsync(token);
            foreach (var device in devices)
            {
                await device.SetStaticAsync(RandomColor(), token);
                await device.SetMultipleColorAsync(RandomColor(), RandomColor(), RandomColor(), RandomColor(), token);
                await device.SetBreathingAsync(RandomColor(), 5, token);
                await device.SetStarAsync(RandomColor(), 5, token);
                await device.SetColorCycleAsync(5, 5, token);
            }
        }

        static Color RandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }
    }
}
