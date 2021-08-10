using System.Threading;
using System.Threading.Tasks;
using Device.Net;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController
{
    public class LedControllerDevice : LedControllerDevice<IDevice>
    {
        public LedControllerDevice(IDevice device) : base(device)
        {
        }

        protected override async Task WriteAsync(byte[] data, CancellationToken token = default)
        {
            await Device.WriteAsync(data, token);
        }
    }
}
