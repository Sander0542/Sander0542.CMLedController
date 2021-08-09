using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Requests;

namespace Sander0542.CMLedController
{
    public abstract class LedControllerDevice<TDevice> : ILedControllerDevice
    {
        protected TDevice Device;

        public LedControllerDevice(TDevice device)
        {
            Device = device;
        }

        public async Task SetStaticAsync(Color color, CancellationToken token = default)
        {
            await SendRequestAsync(new StaticColorRequest(color), token);
        }

        public async Task SetMultipleColorAsync(Color port1, Color port2, Color port3, Color port4, CancellationToken token = default)
        {
            await SendRequestAsync(new MultipleColorRequest(port1, port2, port3, port4), token);
        }

        public async Task SetBreathingAsync(Color color, int speed, CancellationToken token = default)
        {
            await SendRequestAsync(new BreathingRequest(color, speed), token);
        }

        public async Task SetStarAsync(Color color, int speed, CancellationToken token = default)
        {
            await SendRequestAsync(new StarRequest(color, speed), token);
        }

        public async Task SetColorCycleAsync(int brightness, int speed, CancellationToken token = default)
        {
            await SendRequestAsync(new ColorCycleRequest(brightness, speed), token);
        }

        public async Task SendRequestAsync(IRequest request, CancellationToken token = default)
        {
            foreach (var data in request.BuildRequest())
            {
                await WriteAsync(data, token);
            }
        }

        protected abstract Task WriteAsync(byte[] data, CancellationToken token = default);
    }

}
