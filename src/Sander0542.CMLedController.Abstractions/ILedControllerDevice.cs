using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Sander0542.CMLedController.Abstractions
{
    public interface ILedControllerDevice
    {
        public Task SetStaticAsync(Color color, CancellationToken token = default);

        public Task SetMultipleColorAsync(Color port1, Color port2, Color port3, Color port4, CancellationToken token = default);

        public Task SetBreathingAsync(Color color, int speed, CancellationToken token = default);

        public Task SetStarAsync(Color color, int speed, CancellationToken token = default);

        public Task SetColorCycleAsync(int brightness, int speed, CancellationToken token = default);

        public Task SendRequestAsync(IRequest request, CancellationToken token = default);
    }
}
