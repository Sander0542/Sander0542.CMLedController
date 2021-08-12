using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Tests.Shared.Fixtures
{
    public class LedControllerProviderFixture<TProvider> where TProvider : ILedControllerProvider, new()
    {
        public ILedControllerProvider Provider => new TProvider();
    }
}
