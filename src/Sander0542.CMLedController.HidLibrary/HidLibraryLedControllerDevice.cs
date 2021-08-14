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

        protected override async Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default)
        {
            Device.Write(data);
            return Device.Read().Data;
        }

        public override void Dispose()
        {
            Device.Dispose();
        }
    }
}
