using Sander0542.CMLedController.Abstractions.Enums;
using Xunit;

namespace Sander0542.CMLedController.Abstractions.Tests
{
    public class CastableTests
    {
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Call_ToString_ReturnsValue(byte value)
        {
            var mode = (Mode)value;

            Assert.Equal(value.ToString(), mode.ToString());
        }
        
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Call_GetHashCode_ReturnsSame(byte value)
        {
            var mode = (Mode)value;

            Assert.Equal(value.GetHashCode(), mode.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_Mode(byte before)
        {
            var mode = (Mode)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCode(byte before)
        {
            var mode = (OpCode)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCodeFlow(byte before)
        {
            var mode = (OpCodeFlow)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCodeType(byte before)
        {
            var mode = (OpCodeType)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_PacketOffset(byte before)
        {
            var mode = (PacketOffset)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_Speed(byte before)
        {
            var mode = (Speed)before;

            var after = (byte)mode;

            Assert.Equal(before, after);
        }
    }
}
