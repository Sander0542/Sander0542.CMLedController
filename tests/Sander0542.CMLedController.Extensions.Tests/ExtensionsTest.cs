using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Sander0542.CMLedController.Abstractions.Enums;
using Sander0542.CMLedController.Tests.Shared;
using Xunit;

namespace Sander0542.CMLedController.Extensions.Tests
{
    public class ExtensionsTest
    {
        [Theory]
        [InlineData(KnownColor.Red)]
        [InlineData(KnownColor.Green)]
        [InlineData(KnownColor.Blue)]
        [InlineData(KnownColor.White)]
        public async Task Set_ModeStatic_CallsSetMode(KnownColor knownColor)
        {
            var color = Color.FromKnownColor(knownColor);

            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetStaticAsync(color);

            mockController.Verify(device => device.SetModeAsync(Mode.Static, Speed.None, color.A, color, color, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(KnownColor.Red, KnownColor.Green, KnownColor.Blue, KnownColor.White)]
        [InlineData(KnownColor.Green, KnownColor.Blue, KnownColor.White, KnownColor.Red)]
        [InlineData(KnownColor.Blue, KnownColor.White, KnownColor.Red, KnownColor.Green)]
        [InlineData(KnownColor.White, KnownColor.Red, KnownColor.Green, KnownColor.Blue)]
        public async Task Set_ModeMultipleColor_CallsSetMode(KnownColor knownColor1, KnownColor knownColor2, KnownColor knownColor3, KnownColor knownColor4)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);
            var color3 = Color.FromKnownColor(knownColor3);
            var color4 = Color.FromKnownColor(knownColor4);

            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetMultipleColorAsync(color1, color2, color3, color4);

            mockController.Verify(device => device.SetLedsDirectAsync(color1, color2, color3, color4, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(KnownColor.Red, 0x3C)]
        [InlineData(KnownColor.Green, 0x34)]
        [InlineData(KnownColor.Blue, 0x2D)]
        [InlineData(KnownColor.White, 0x26)]
        public async Task Set_ModeBreathing_CallsSetMode(KnownColor knownColor, byte speed)
        {
            var color = Color.FromKnownColor(knownColor);

            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetBreathingAsync(color, speed);

            mockController.Verify(device => device.SetModeAsync(Mode.Breathing, speed, color.A, color, color, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(KnownColor.Red, KnownColor.Green, 0x26, 0x46)]
        [InlineData(KnownColor.Green, KnownColor.Blue, 0x2D, 0x3F)]
        [InlineData(KnownColor.Blue, KnownColor.White, 0x34, 0x38)]
        [InlineData(KnownColor.White, KnownColor.Red, 0x3C, 0x32)]
        public async Task Set_ModeStar_CallsSetMode(KnownColor knownColor1, KnownColor knownColor2, byte speed, byte brightness)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);

            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetStarAsync(color1, color2, speed, brightness);

            mockController.Verify(device => device.SetModeAsync(Mode.Star, speed, brightness, color1, color2, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(0x96, 0x3F)]
        [InlineData(0x86, 0x74)]
        [InlineData(0x77, 0xA9)]
        [InlineData(0x68, 0xDF)]
        public async Task Set_ModeColorCycle_CallsSetMode(byte speed, byte brightness)
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetColorCycleAsync(speed, brightness);

            mockController.Verify(device => device.SetModeAsync(Mode.ColorCycle, speed, brightness, Color.White, Color.Black, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Set_ModeOff_CallsSetMode()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var mockController = new Mock<TestLedControllerDevice>(mockDevice.Object);

            var controller = mockController.Object;

            await controller.SetOffAsync();

            mockController.Verify(device => device.SetModeAsync(Mode.Off, Speed.None, 0x03, Color.Black, Color.Black, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
