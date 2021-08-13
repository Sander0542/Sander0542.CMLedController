using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions;
using Sander0542.CMLedController.Abstractions.Enums;
using Sander0542.CMLedController.Extensions;
using Sander0542.CMLedController.RealDevice.Tests.Attributes;
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

        [IgnoreWhenNoDeviceFact]
        public async Task Call_GetControllersAsync_ReturnsDevice()
        {
            var controllers = await _provider.GetControllersAsync();

            Assert.NotEmpty(controllers);
        }

        [IgnoreWhenNoDeviceTheory]
        [InlineData(0x00, 0x05, 0xFF, KnownColor.Red, KnownColor.Red)]// Static
        [InlineData(0x01, 0x26, 0xFF, KnownColor.Green, KnownColor.Green)]// Breathing
        [InlineData(0x02, 0x68, 0xDF, KnownColor.White, KnownColor.Black)]// Color Cycle
        [InlineData(0x03, 0x32, 0xFF, KnownColor.Blue, KnownColor.White)]// Star
        [InlineData(0xFE, 0x00, 0x00, KnownColor.Black, KnownColor.Black)]// Off
        public async Task Call_SetMode_UpdatesConfig(byte byteMode, byte speed, byte brightness, KnownColor knownColor1, KnownColor knownColor2)
        {
            var mode = (Mode)byteMode;
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);

            var controller = (await _provider.GetControllersAsync()).First();

            await controller.SetModeAsync(mode, speed, brightness, color1, color2);

            Assert.Equal(mode, controller.Mode);

            await controller.ReadCurrentModeAsync();

            Assert.Equal(mode, controller.Mode);

            await controller.ReadModeConfigAsync(mode);

            Assert.Equal(mode, controller.Mode);
            Assert.Equal(speed, controller.Speed);
            Assert.Equal(brightness, controller.Brightness);
            Assert.Equal(color1.ToArgb(), controller.GetModeColor(0).ToArgb());
            Assert.Equal(color2.ToArgb(), controller.GetModeColor(1).ToArgb());
        }

        [IgnoreWhenNoDeviceFact]
        public async Task Call_SetLedsDirect_UpdatesConfig()
        {
            var mode = Mode.Multiple;
            var color1 = Color.Red;
            var color2 = Color.Green;
            var color3 = Color.Blue;
            var color4 = Color.White;

            var controller = (await _provider.GetControllersAsync()).First();

            await controller.SetLedsDirectAsync(color1, color2, color3, color4);

            Assert.Equal(mode, controller.Mode);

            await controller.ReadCurrentModeAsync();

            Assert.Equal(mode, controller.Mode);

            await controller.ReadModeConfigAsync(mode);

            Assert.Equal(mode, controller.Mode);
            Assert.Equal(0x00, controller.Speed);
            Assert.Equal(0xFF, controller.Brightness);
            Assert.Equal(color1.ToArgb(), controller.GetPortColor(0).ToArgb());
            Assert.Equal(color2.ToArgb(), controller.GetPortColor(1).ToArgb());
            Assert.Equal(color3.ToArgb(), controller.GetPortColor(2).ToArgb());
            Assert.Equal(color4.ToArgb(), controller.GetPortColor(3).ToArgb());
        }
    }
}
