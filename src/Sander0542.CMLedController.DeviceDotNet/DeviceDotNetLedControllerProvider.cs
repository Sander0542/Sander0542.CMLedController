using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Device.Net;
using Device.Net.LibUsb;
using Hid.Net.Windows;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.DeviceDotNet
{
    public class DeviceDotNetLedControllerProvider : ILedControllerProvider
    {
        private readonly IDeviceFactory _deviceFactory;
        
        public DeviceDotNetLedControllerProvider()
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
            var devices = new List<DeviceDotNetLedControllerDevice>();

            foreach (var definition in definitions)
            {
                var device = await _deviceFactory.GetDeviceAsync(definition, token);
                await device.InitializeAsync(token);
                devices.Add(new DeviceDotNetLedControllerDevice(device));
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
            var filter = GetFilter();

            var hidFactory = filter.CreateWindowsHidDeviceFactory();
            var libUsbFactory = filter.CreateLibUsbDeviceFactory();

            return hidFactory.Aggregate(libUsbFactory);
        }
    }
}
