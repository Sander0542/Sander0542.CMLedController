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
            Mode mode = value;

            Assert.Equal(value.ToString(), mode.ToString());
        }
        
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Call_GetHashCode_ReturnsSame(byte value)
        {
            Mode mode = value;

            byte value2 = mode;

            Assert.Equal(value, value2);
            Assert.Equal(value.GetHashCode(), value2.GetHashCode());
        }
        
        [Fact]
        public void Cast_Mode_Byte()
        {
            var mode = Mode.Multilayer;

            byte value = mode;

            Mode mode2 = value;

            Assert.Equal(mode, mode2);
            Assert.Equal(value.GetHashCode(), mode2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_Mode(byte before)
        {
            Mode mode = before;

            byte after = mode;

            Assert.Equal(before, after);
        }
        
        [Fact]
        public void Cast_OpCode_Byte()
        {
            var opCode = OpCode.Write;

            byte value = opCode;

            OpCode opCode2 = value;

            Assert.Equal(opCode, opCode2);
            Assert.Equal(value.GetHashCode(), opCode2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCode(byte before)
        {
            OpCode opCode = before;

            byte after = opCode;

            Assert.Equal(before, after);
        }
        
        [Fact]
        public void Cast_OpCodeFlow_Byte()
        {
            var opCodeFlow = OpCodeFlow.Flow01;

            byte value = opCodeFlow;

            OpCodeFlow opCodeFlow2 = value;

            Assert.Equal(opCodeFlow, opCodeFlow2);
            Assert.Equal(value.GetHashCode(), opCodeFlow2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCodeFlow(byte before)
        {
            OpCodeFlow opCodeFlow = before;

            byte after = opCodeFlow;

            Assert.Equal(before, after);
        }
        
        [Fact]
        public void Cast_OpCodeType_Byte()
        {
            var opCodeType = OpCodeType.ConfigFull;

            byte value = opCodeType;

            OpCodeType opCodeType2 = value;

            Assert.Equal(opCodeType, opCodeType2);
            Assert.Equal(value.GetHashCode(), opCodeType2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_OpCodeType(byte before)
        {
            OpCodeType opCodeType = before;

            byte after = opCodeType;

            Assert.Equal(before, after);
        }
        
        [Fact]
        public void Cast_PacketOffset_Byte()
        {
            var packetOffset = PacketOffset.Speed;

            byte value = packetOffset;

            PacketOffset packetOffset2 = value;

            Assert.Equal(packetOffset, packetOffset2);
            Assert.Equal(value.GetHashCode(), packetOffset2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_PacketOffset(byte before)
        {
            PacketOffset packetOffset = before;

            byte after = packetOffset;

            Assert.Equal(before, after);
        }
        
        [Fact]
        public void Cast_Speed_Byte()
        {
            var speed = Speed.BreathingFastest;

            byte value = speed;

            Speed speed2 = value;

            Assert.Equal(speed, speed2);
            Assert.Equal(value.GetHashCode(), speed2.GetHashCode());
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3A)]
        [InlineData(0x7E)]
        [InlineData(0xCE)]
        public void Cast_Byte_Speed(byte before)
        {
            Speed speed = before;

            byte after = speed;

            Assert.Equal(before, after);
        }
    }
}
