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
        }

        protected override async Task WriteAsync(byte[] data, CancellationToken token = default)
        {
        }
    }
}
