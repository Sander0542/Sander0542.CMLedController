using System;

namespace Sander0542.CMLedController.Tests.Shared
{
    public interface ITestHidDevice : IDisposable
    {
        public byte[] WriteAndRead(byte[] data);
    }
}
