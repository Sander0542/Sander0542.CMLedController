namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class OpCodeFlow : Castable<byte>
    {
        public static readonly OpCodeFlow Flow00 = new OpCodeFlow(0x00);
        public static readonly OpCodeFlow Flow01 = new OpCodeFlow(0x01);
        public static readonly OpCodeFlow Flow80 = new OpCodeFlow(0x80);

        private OpCodeFlow(byte value) : base(value)
        {
        }
        
        public static implicit operator OpCodeFlow(byte value) => new OpCodeFlow(value);
    }
}
