using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Abstractions.Enums;

namespace Sander0542.CMLedController.Extensions
{
    public static class LedControllerDeviceExtensions
    {
        public static async Task SetStaticAsync(this ILedControllerDevice device, Color color, CancellationToken token = default)
        {
            await device.SetModeAsync(Mode.Static, Speed.None, color.A, color, color, token);
        }

        public static async Task SetMultipleColorAsync(this ILedControllerDevice device, Color port1, Color port2, Color port3, Color port4, CancellationToken token = default)
        {
            await device.SetLedsDirectAsync(port1, port2, port3, port4, token);
        }

        public static async Task SetBreathingAsync(this ILedControllerDevice device, Color color, byte speed, CancellationToken token = default)
        {
            await device.SetModeAsync(Mode.Breathing, speed, color.A, color, color, token);
        }

        public static async Task SetColorCycleAsync(this ILedControllerDevice device, byte speed, byte brightness, CancellationToken token = default)
        {
            await device.SetModeAsync(Mode.ColorCycle, speed, brightness, Color.White, Color.Black, token);
        }

        public static async Task SetStarAsync(this ILedControllerDevice device, Color color1, Color color2, byte speed, byte brightness, CancellationToken token = default)
        {
            await device.SetModeAsync(Mode.Star, speed, brightness, color1, color2, token);
        }

        public static async Task SetOffAsync(this ILedControllerDevice device, CancellationToken token = default)
        {
            await device.SetModeAsync(Mode.Off, Speed.None, 0x03, Color.Black, Color.Black, token);
        }
    }
}
