using System;
using System.Linq;
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

        protected abstract Task WriteAsync(byte[] data, CancellationToken token = default);

        protected abstract Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default);

        public abstract void Dispose();
        {
        protected byte[] PrepareData(byte[] data)
        {
            if (data.Length == PacketSize)
            {
                return new byte[] { 0x00 }.Concat(data).ToArray();
            }

            if (data.Length == PacketSize + 1 && data[0] == 0x00)
            {
                return data;
            }

            if (data.Length > PacketSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The data of the packet cannot be greater than {PacketSize}");
            }

            var newData = data.ToList();
            while (newData.Count < PacketSize)
            {
                newData.Add(0x00);
            }
            return new byte[] { 0x00 }.Concat(newData).ToArray();
        }
    }
}
