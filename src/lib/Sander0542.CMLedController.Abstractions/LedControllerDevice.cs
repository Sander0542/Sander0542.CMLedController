using System.Threading;
using System.Threading.Tasks;

namespace Sander0542.CMLedController.Abstractions
{
    public abstract class LedControllerDevice<TDevice> : ILedControllerDevice
    {
        protected TDevice Device;

        public LedControllerDevice(TDevice device)
        {
            Device = device;
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
