namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class Speed : Castable<byte>
    {
        public static readonly Speed None = new Speed(0x05);

        public static readonly Speed BreathingSlowest = new Speed(0x3C);
        public static readonly Speed BreathingFastest = new Speed(0x26);

        public static readonly Speed ColorCycleSlowest = new Speed(0x96);
        public static readonly Speed ColorCycleFastest = new Speed(0x68);

        public static readonly Speed StarSlowest = new Speed(0x46);
        public static readonly Speed StarFastest = new Speed(0x32);

        private Speed(byte value) : base(value)
        {
        }
        
        public static implicit operator Speed(byte value) => new Speed(value);
    }
}
