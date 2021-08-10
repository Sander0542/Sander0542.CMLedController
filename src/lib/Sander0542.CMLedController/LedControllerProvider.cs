using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Device.Net;
using Hid.Net.Windows;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController
{
    public class LedControllerProvider : ILedControllerProvider
    {
        private readonly IDeviceFactory _deviceFactory;

        public LedControllerProvider()
        {
            _deviceFactory = GetFactory();
        }

        public async Task<IEnumerable<ILedControllerDevice>> GetControllersAsync(CancellationToken token = default)
        {
            var devices = await GetDevicesAsync(token);

            return await MapDevicesAsync(devices, token);
        }

        private async Task<IEnumerable<ILedControllerDevice>> MapDevicesAsync(IEnumerable<ConnectedDeviceDefinition> definitions, CancellationToken token = default)
        {
            var devices = new List<LedControllerDevice>();

            foreach (var definition in definitions)
            {
                var device = await _deviceFactory.GetDeviceAsync(definition, token);
                await device.InitializeAsync(token);
                devices.Add(new LedControllerDevice(device));
            }

            return devices;
        }

        private async Task<IEnumerable<ConnectedDeviceDefinition>> GetDevicesAsync(CancellationToken token = default)
        {
            return await _deviceFactory.GetConnectedDeviceDefinitionsAsync(token).ConfigureAwait(false);
        }

        private FilterDeviceDefinition GetFilter()
        {
            return new FilterDeviceDefinition(Constants.VendorId, Constants.ProductId, Constants.UsagePage);
        }

        private IDeviceFactory GetFactory()
        {
            return GetFilter().CreateWindowsHidDeviceFactory();
        }
    }
}
