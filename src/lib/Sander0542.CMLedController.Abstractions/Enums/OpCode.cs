namespace Sander0542.CMLedController.Abstractions.Enums
{
    public sealed class OpCode : Castable<byte>
    {
        public static readonly OpCode FlowControl = new OpCode(0x41);
        public static readonly OpCode Unknown50 = new OpCode(0x50);
        public static readonly OpCode Write = new OpCode(0x51);
        public static readonly OpCode Read = new OpCode(0x52);

        private OpCode(byte value) : base(value)
        {
        }
        
        public static implicit operator OpCode(byte value) => new OpCode(value);
    }
}
