using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Tests.Shared
{
    public class TestLedControllerDevice : LedControllerDevice<ITestHidDevice>
    {
        public TestLedControllerDevice(ITestHidDevice device) : base(device)
        {
        }

        protected override async Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default)
        {
            return Device.WriteAndRead(data);
        }

        public override void Dispose()
        {
            Device.Dispose();
        }
    }
}
