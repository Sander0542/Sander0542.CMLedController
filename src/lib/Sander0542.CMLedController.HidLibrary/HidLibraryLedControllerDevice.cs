using System.Threading;
using System.Threading.Tasks;
using HidLibrary;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.HidLibrary
{
    public class HidLibraryLedControllerDevice : LedControllerDevice<HidDevice>
    {
        public HidLibraryLedControllerDevice(HidDevice device) : base(device)
        {
        }

        protected override async Task WriteAsync(byte[] data, CancellationToken token = default)
        {
            await Task.Run(() => Device.Write(data), token);
        }
    }
}
