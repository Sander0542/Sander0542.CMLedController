using System;
using System.Linq;

namespace Sander0542.CMLedController.Abstractions.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] PreparePacket(this byte[] data)
        {
            if (data.Length == Constants.PacketSize)
            {
                return new byte[] { 0x00 }.Concat(data).ToArray();
            }

            if (data.Length == Constants.PacketSize + 1 && data[0] == 0x00)
            {
                return data;
            }

            if (data.Length > Constants.PacketSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The data of the packet cannot be greater than {Constants.PacketSize}");
            }

            var newData = data.ToList();
            while (newData.Count < Constants.PacketSize)
            {
                newData.Add(0x00);
            }
            return new byte[] { 0x00 }.Concat(newData).ToArray();
        }
    }
}
