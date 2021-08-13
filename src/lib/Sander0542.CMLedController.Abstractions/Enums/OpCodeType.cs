namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class OpCodeType : Castable<byte>
    {
        public static readonly OpCodeType Mode = new OpCodeType(0x28);
        public static readonly OpCodeType ConfigSimplified = new OpCodeType(0x2B);
        public static readonly OpCodeType ConfigFull = new OpCodeType(0x2C);
        public static readonly OpCodeType Unknown30 = new OpCodeType(0x30);
        public static readonly OpCodeType Unknown55 = new OpCodeType(0x55);
        public static readonly OpCodeType LedInfo = new OpCodeType(0xA8);

        private OpCodeType(byte value) : base(value)
        {
        }
        
        public static implicit operator OpCodeType(byte value) => new OpCodeType(value);
    }
}
