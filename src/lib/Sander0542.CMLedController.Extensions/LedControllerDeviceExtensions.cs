using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Requests;

namespace Sander0542.CMLedController.Extensions
{
    public static class LedControllerDeviceExtensions
    {
        public static async Task SetStaticAsync(this ILedControllerDevice device, Color color, CancellationToken token = default)
        {
            await device.SendRequestAsync(new StaticColorRequest(color), token);
        }

        public static async Task SetMultipleColorAsync(this ILedControllerDevice device, Color port1, Color port2, Color port3, Color port4, CancellationToken token = default)
        {
            await device.SendRequestAsync(new MultipleColorRequest(port1, port2, port3, port4), token);
        }

        public static async Task SetBreathingAsync(this ILedControllerDevice device, Color color, int speed, CancellationToken token = default)
        {
            await device.SendRequestAsync(new BreathingRequest(color, speed), token);
        }

        public static async Task SetStarAsync(this ILedControllerDevice device, Color color, int speed, CancellationToken token = default)
        {
            await device.SendRequestAsync(new StarRequest(color, speed), token);
        }

        public static async Task SetColorCycleAsync(this ILedControllerDevice device, int brightness, int speed, CancellationToken token = default)
        {
            await device.SendRequestAsync(new ColorCycleRequest(brightness, speed), token);
        }
    }
}
