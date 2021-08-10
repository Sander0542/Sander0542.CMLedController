using System.Collections.Generic;

namespace Sander0542.CMLedController.Abstractions
{
    public class DataRow : List<byte>
    {
        public byte Filler { get; }
        
        public DataRow(byte filler = 0) : base(64)
        {
            Filler = filler;
        }
    }
}
