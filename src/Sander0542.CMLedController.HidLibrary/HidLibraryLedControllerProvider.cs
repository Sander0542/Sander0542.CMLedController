using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HidLibrary;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.HidLibrary
{
    public class HidLibraryLedControllerProvider : ILedControllerProvider
    {
        public async Task<IEnumerable<ILedControllerDevice>> GetControllersAsync(CancellationToken token = default)
        {
            var hidDevices = HidDevices.Enumerate(Constants.VendorId, Constants.ProductId, Constants.UsagePage);

            return hidDevices.Select(device => new HidLibraryLedControllerDevice(device));
        }
    }
}
