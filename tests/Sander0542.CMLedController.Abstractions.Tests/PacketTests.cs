using System.Drawing;
using Sander0542.CMLedController.Abstractions.Enums;
using Xunit;

namespace Sander0542.CMLedController.Abstractions.Tests
{
    public class PacketTests
    {
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

            Assert.Equal(color.R, packet.Get(PacketOffset.MultipleColor4));
            Assert.Equal(color.G, packet.Get(PacketOffset.MultipleColor4 + 1));
            Assert.Equal(color.B, packet.Get(PacketOffset.MultipleColor4 + 2));
        }
    }
}
