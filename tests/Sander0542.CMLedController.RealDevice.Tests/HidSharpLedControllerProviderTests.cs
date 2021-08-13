using Sander0542.CMLedController.HidSharp;
using Sander0542.CMLedController.Tests.Shared.Fixtures;

namespace Sander0542.CMLedController.RealDevice.Tests
{
    public class HidSharpLedControllerProviderTests : LedControllerProviderTests<HidSharpLedControllerProvider>
    {
        public HidSharpLedControllerProviderTests(LedControllerProviderFixture<HidSharpLedControllerProvider> providerFixture) : base(providerFixture)
        {
        }
    }
}
