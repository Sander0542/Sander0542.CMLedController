using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Tests.Shared.Fixtures;
using Xunit;

namespace Sander0542.CMLedController.RealDevice.Tests
{
    public class LedControllerProviderTests : LedControllerProviderTests<LedControllerProvider>
    {
        public LedControllerProviderTests(LedControllerProviderFixture<LedControllerProvider> providerFixture) : base(providerFixture)
        {
        }
    }

    [Collection("RealDevice")]
    public abstract class LedControllerProviderTests<TProvider> : IClassFixture<LedControllerProviderFixture<TProvider>> where TProvider : ILedControllerProvider, new()
    {
        private readonly ILedControllerProvider _provider;

        public LedControllerProviderTests(LedControllerProviderFixture<TProvider> providerFixture)
        {
            _provider = providerFixture.Provider;
        }
    }
}
