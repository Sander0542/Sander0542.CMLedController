namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class Mode : Castable<byte>
    {
        public static readonly Mode Static = new Mode(0x00);
        public static readonly Mode Breathing = new Mode(0x01);
        public static readonly Mode ColorCycle = new Mode(0x02);
        public static readonly Mode Star = new Mode(0x03);
        public static readonly Mode Multiple = new Mode(0x04);
        public static readonly Mode Multilayer = new Mode(0xE0);
        public static readonly Mode Off = new Mode(0xFE);

        private Mode(byte value) : base(value)
        {
        }
        
        public static implicit operator Mode(byte value) => new Mode(value);
    }
}
