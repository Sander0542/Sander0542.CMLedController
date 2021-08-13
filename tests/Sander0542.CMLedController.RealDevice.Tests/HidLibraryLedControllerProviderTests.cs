using Sander0542.CMLedController.HidLibrary;
using Sander0542.CMLedController.Tests.Shared.Fixtures;

namespace Sander0542.CMLedController.RealDevice.Tests
{
    public class HidLibraryLedControllerProviderTests : LedControllerProviderTests<HidLibraryLedControllerProvider>
    {
        public HidLibraryLedControllerProviderTests(LedControllerProviderFixture<HidLibraryLedControllerProvider> providerFixture) : base(providerFixture)
        {
        }
    }
}
