namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class PacketOffset : Castable<byte>
    {
        public static readonly PacketOffset Op = new PacketOffset(0x00);
        public static readonly PacketOffset Type = new PacketOffset(0x01);
        public static readonly PacketOffset Multilayer = new PacketOffset(0x02);
        public static readonly PacketOffset Mode = new PacketOffset(0x04);
        public static readonly PacketOffset Speed = new PacketOffset(0x05);
        public static readonly PacketOffset Brightness = new PacketOffset(0x09);
        public static readonly PacketOffset Color1 = new PacketOffset(0x0A);
        public static readonly PacketOffset Color2 = new PacketOffset(0x0D);
        public static readonly PacketOffset MultipleColor1 = new PacketOffset(0x04);
        public static readonly PacketOffset MultipleColor2 = new PacketOffset(0x07);
        public static readonly PacketOffset MultipleColor3 = new PacketOffset(0x0A);
        public static readonly PacketOffset MultipleColor4 = new PacketOffset(0x0D);

        private PacketOffset(byte value) : base(value)
        {
        }
        
        public static implicit operator PacketOffset(byte value) => new PacketOffset(value);
    }
}
