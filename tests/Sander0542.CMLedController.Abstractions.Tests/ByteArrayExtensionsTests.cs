using System;
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
    }
}
