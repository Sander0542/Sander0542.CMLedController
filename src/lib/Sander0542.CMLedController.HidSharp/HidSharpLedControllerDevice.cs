using System;
using System.Threading;
using System.Threading.Tasks;
using HidSharp;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.HidSharp
{
    public class HidSharpLedControllerDevice : LedControllerDevice<HidDevice>
    {
        private readonly HidStream _stream;

        public HidSharpLedControllerDevice(HidDevice device) : base(device)
        {
            if (!Device.TryOpen(out _stream))
            {
                throw new InvalidOperationException("Could not open stream to Led Controller");
            }
        }

        protected override async Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default)
        {
            await _stream.WriteAsync(data, token);
            await _stream.ReadAsync(data, token);

            return data;
        }

        public override void Dispose()
        {
            _stream.Dispose();
        }
    }
}
