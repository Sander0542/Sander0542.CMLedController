using System;
using System.Drawing;
using System.Linq;
using Sander0542.CMLedController.Abstractions.Enums;

namespace Sander0542.CMLedController.Abstractions.Extensions
{
    public static class ByteArrayExtensions
    {
        public static void SetColor(this byte[] data, PacketOffset packetOffset, Color color)
        {
            data[packetOffset] = color.R;
            data[packetOffset + 1] = color.G;
            data[packetOffset + 2] = color.B;
        }

        public static Color GetColor(this byte[] data, PacketOffset packetOffset)
        {
            return Color.FromArgb(
                data[packetOffset],
                data[packetOffset + 1],
                data[packetOffset + 2]
            );
        }

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

        public static byte[] PrepareResponse(this byte[] data)
        {
            if (data.Length == Constants.PacketSize)
            {
                return data;
            }

            if (data.Length == Constants.PacketSize + 1 && data[0] == 0x00)
            {
                return data.TakeLast(Constants.PacketSize).ToArray();
            }

            if (data.Length > Constants.PacketSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The data of the response cannot be greater than {Constants.PacketSize}");
            }

            var newData = data.ToList();
            while (newData.Count < Constants.PacketSize)
            {
                newData.Add(0x00);
            }
            return newData.ToArray();
        }
    }
}
