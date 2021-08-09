using System.Threading;
using System.Threading.Tasks;
using Device.Net;

namespace Sander0542.CMLedController.DeviceDotNet
{
    public class DeviceDotNetLedControllerDevice : LedControllerDevice<IDevice>
    {
        public DeviceDotNetLedControllerDevice(IDevice device) : base(device)
        {
        }

        protected override async Task WriteAsync(byte[] data, CancellationToken token = default)
        {
            await Device.WriteAsync(data, token);
        }
    }
}
