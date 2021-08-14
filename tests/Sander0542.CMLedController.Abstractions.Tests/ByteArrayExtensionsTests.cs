using System;
using System.Drawing;
using Sander0542.CMLedController.Abstractions.Extensions;
using Xunit;

namespace Sander0542.CMLedController.Abstractions.Tests
{
    public class ByteArrayExtensionsTests
    {
        [Fact]
        public void Call_PrepareData_AddsPrefix()
        {
            var data = new byte[Constants.PacketSize];

            var result = data.PreparePacket();

            Assert.Equal(Constants.PacketSize + 1, result.Length);
            Assert.Equal(0x00, result[0]);
        }

        [Fact]
        public void Call_PrepareData_ReturnsSame()
        {
            var data = new byte[Constants.PacketSize + 1];

            var result = data.PreparePacket();

            Assert.Same(data, result);
            Assert.Equal(0x00, result[0]);
        }

        [Fact]
        public void Call_PrepareData_TooLong_ThrowsError()
        {
            var data = new byte[Constants.PacketSize + 2];

            Assert.Throws<ArgumentOutOfRangeException>(() => data.PreparePacket());
        }

        [Fact]
        public void Call_PrepareData_TooShort_ReturnsRightSize()
        {
            var data = new byte[Constants.PacketSize - 10];

            var result = data.PreparePacket();

            Assert.Equal(Constants.PacketSize + 1, result.Length);
            Assert.Equal(0x00, result[0]);
        }

        [Fact]
        public void Call_PrepareResponse_ReturnsSame()
        {
            var data = new byte[Constants.PacketSize];

            var result = data.PrepareResponse();

            Assert.Same(data, result);
        }

        [Theory]
        [InlineData(0xF2)]
        [InlineData(0x3C)]
        [InlineData(0x81)]
        [InlineData(0xAF)]
        public void Call_PrepareResponse_RemovesPrefix(byte value)
        {
            var data = new byte[Constants.PacketSize + 1];
            data[1] = value;

            var result = data.PrepareResponse();

            Assert.Equal(Constants.PacketSize, result.Length);
            Assert.Equal(value, result[0]);
        }

        [Fact]
        public void Call_PrepareResponse_TooLong_ThrowsError()
        {
            var data = new byte[Constants.PacketSize + 2];

            Assert.Throws<ArgumentOutOfRangeException>(() => data.PrepareResponse());
        }

        [Fact]
        public void Call_PrepareResponse_TooShort_ReturnsRightSize()
        {
            var data = new byte[Constants.PacketSize - 10];

            var result = data.PrepareResponse();

            Assert.Equal(Constants.PacketSize, result.Length);
        }

        [Theory]
        [InlineData(0x4A, KnownColor.Red)]
        [InlineData(0x1A, KnownColor.Green)]
        [InlineData(0x26, KnownColor.Blue)]
        [InlineData(0x37, KnownColor.White)]
        public void Call_SetColor_GetColor_ReturnsColor(byte index, KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);

            var data = new byte[index + 3];
            data.SetColor(index, color);
            
            Assert.Equal(color.ToArgb(), data.GetColor(index).ToArgb());
        }
    }
}
