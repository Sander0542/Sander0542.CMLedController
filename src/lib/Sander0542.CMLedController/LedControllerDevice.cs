using System;
using System.Linq;
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
            await Device.WriteAsync(PrepareData(data), token);
        }

        protected override async Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default)
        {
            return (await Device.WriteAndReadAsync(PrepareData(data), token)).Data;
        }

        public override void Dispose()
        {
            Device.Dispose();
        }
    }
}
