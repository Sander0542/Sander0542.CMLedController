using System.Drawing;
using Sander0542.CMLedController.Abstractions.Enums;
using Xunit;

namespace Sander0542.CMLedController.Abstractions.Tests
{
    public class PacketTests
    {
        [Fact]
        public void Get_Length_ReturnsPacketSize()
        {
            var packet = new Packet();
            
            Assert.Equal(Constants.PacketSize, packet.Length);
        }
        
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Call_Set_Get_UpdatesReturnsValue_PacketOffset(byte value)
        {
            var packet = new Packet();
            packet.Set(PacketOffset.Op, value);

            Assert.Equal(value, packet.Get(PacketOffset.Op));
        }
        
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Call_Set_Get_UpdatesReturnsValue_Index(byte value)
        {
            var packet = new Packet();
            packet.Set(5, value);

            Assert.Equal(value, packet.Get(5));
        }
        
        [Theory]
        [InlineData(5, 0x5E)]
        [InlineData(13, 0xA3)]
        [InlineData(22, 0x9C)]
        [InlineData(34, 0x2F)]
        [InlineData(47, 0x00)]
        [InlineData(62, 0xFF)]
        public void Convert_Packet_ReturnsSame(int index, byte value)
        {
            var packet = new Packet();
            packet.Set(index, value);

            byte[] data = packet;

            Packet packet2 = data;

            Assert.Equal(packet, packet2);
            Assert.Equal(packet.GetHashCode(), packet2.GetHashCode());
            Assert.Equal(value, packet2.Get(index));
        }
        
        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_Operation_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                Operation = value
            };

            Assert.Equal(value, (byte)packet.Operation);
            Assert.Equal(value, packet.Get(PacketOffset.Op));
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_OperationType_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                OperationType = value
            };

            Assert.Equal(value, (byte)packet.OperationType);
            Assert.Equal(value, packet.Get(PacketOffset.Type));
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_Multilayer_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                Multilayer = value
            };

            Assert.Equal(value, packet.Multilayer);
            Assert.Equal(value, packet.Get(PacketOffset.Multilayer));
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_Mode_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                Mode = value
            };

            Assert.Equal(value, (byte)packet.Mode);
            Assert.Equal(value, packet.Get(PacketOffset.Mode));
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_Speed_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                Speed = value
            };

            Assert.Equal(value, packet.Speed);
            Assert.Equal(value, packet.Get(PacketOffset.Speed));
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x3E)]
        [InlineData(0xA8)]
        [InlineData(0xEA)]
        public void Set_Brightness_UpdatesArray(byte value)
        {
            var packet = new Packet
            {
                Brightness = value
            };

            Assert.Equal(value, packet.Brightness);
            Assert.Equal(value, packet.Get(PacketOffset.Brightness));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_Color1_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                Color1 = color
            };
            
            Assert.Equal(color.ToArgb(), packet.Color1.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.Color1));
            Assert.Equal(color.G, packet.Get(PacketOffset.Color1 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.Color1 + 2));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_Color2_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                Color2 = color
            };

            Assert.Equal(color.ToArgb(), packet.Color2.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.Color2));
            Assert.Equal(color.G, packet.Get(PacketOffset.Color2 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.Color2 + 2));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_MultipleColor1_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                MultipleColor1 = color
            };

            Assert.Equal(color.ToArgb(), packet.MultipleColor1.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.MultipleColor1));
            Assert.Equal(color.G, packet.Get(PacketOffset.MultipleColor1 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.MultipleColor1 + 2));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_MultipleColor2_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                MultipleColor2 = color
            };

            Assert.Equal(color.ToArgb(), packet.MultipleColor2.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.MultipleColor2));
            Assert.Equal(color.G, packet.Get(PacketOffset.MultipleColor2 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.MultipleColor2 + 2));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_MultipleColor3_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                MultipleColor3 = color
            };

            Assert.Equal(color.ToArgb(), packet.MultipleColor3.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.MultipleColor3));
            Assert.Equal(color.G, packet.Get(PacketOffset.MultipleColor3 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.MultipleColor3 + 2));
        }

        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public void Set_MultipleColor4_UpdatesArray(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);
            var packet = new Packet
            {
                MultipleColor4 = color
            };

            Assert.Equal(color.ToArgb(), packet.MultipleColor4.ToArgb());
            Assert.Equal(color.R, packet.Get(PacketOffset.MultipleColor4));
            Assert.Equal(color.G, packet.Get(PacketOffset.MultipleColor4 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.MultipleColor4 + 2));
        }
    }
}
