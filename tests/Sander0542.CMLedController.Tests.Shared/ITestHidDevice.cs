using System;

namespace Sander0542.CMLedController.Tests.Shared
{
    public interface ITestHidDevice : IDisposable
    {
        public void Write(byte[] data);

        public byte[] WriteAndRead(byte[] data);
    }
}
