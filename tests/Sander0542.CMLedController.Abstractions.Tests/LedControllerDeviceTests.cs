using System.Drawing;
using System.Threading.Tasks;
using Moq;
using Sander0542.CMLedController.Abstractions.Enums;
using Sander0542.CMLedController.Abstractions.Extensions;
using Sander0542.CMLedController.Tests.Shared;
using Xunit;

namespace Sander0542.CMLedController.Abstractions.Tests
{
    public class LedControllerDeviceTests
    {
        [Fact]
        public async Task Call_SetMode_SetsMode()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.SetModeAsync(Mode.Breathing, Speed.None, 0xFF, Color.Red, Color.Blue);

            Assert.Equal(Mode.Breathing, controller.Mode);
            mockDevice.Verify(device => device.Write(It.IsAny<byte[]>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Call_SetMode_SetsSpeed()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.SetModeAsync(Mode.Breathing, Speed.BreathingFastest, 0xFF, Color.Red, Color.Blue);

            Assert.Equal((byte)Speed.BreathingFastest, controller.Speed);
            mockDevice.Verify(device => device.Write(It.IsAny<byte[]>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Call_SetMode_SetsBrightness()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.SetModeAsync(Mode.Breathing, Speed.None, 0xEF, Color.Red, Color.Blue);

            Assert.Equal(0xEF, controller.Brightness);
            mockDevice.Verify(device => device.Write(It.IsAny<byte[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(KnownColor.Red, KnownColor.Green)]
        [InlineData(KnownColor.Green, KnownColor.Blue)]
        [InlineData(KnownColor.Blue, KnownColor.White)]
        [InlineData(KnownColor.White, KnownColor.Red)]
        public async Task Call_SetMode_SetsColorMode(KnownColor knownColor1, KnownColor knownColor2)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);

            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.SetModeAsync(Mode.Star, Speed.StarFastest, 0xFF, color1, color2);

            Assert.Equal(color1, controller.GetModeColor(0));
            Assert.Equal(color2, controller.GetModeColor(1));
            mockDevice.Verify(device => device.Write(It.IsAny<byte[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(KnownColor.Red, KnownColor.Green, KnownColor.Blue, KnownColor.White)]
        [InlineData(KnownColor.Green, KnownColor.Blue, KnownColor.White, KnownColor.Red)]
        [InlineData(KnownColor.Blue, KnownColor.White, KnownColor.Red, KnownColor.Green)]
        [InlineData(KnownColor.White, KnownColor.Red, KnownColor.Green, KnownColor.Blue)]
        public async Task Call_SetMode_SetsPortColor(KnownColor knownColor1, KnownColor knownColor2, KnownColor knownColor3, KnownColor knownColor4)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);
            var color3 = Color.FromKnownColor(knownColor3);
            var color4 = Color.FromKnownColor(knownColor4);

            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.SetLedsDirectAsync(color1, color2, color3, color4);

            Assert.Equal(color1, controller.GetPortColor(0));
            Assert.Equal(color2, controller.GetPortColor(1));
            Assert.Equal(color3, controller.GetPortColor(2));
            Assert.Equal(color4, controller.GetPortColor(3));
            mockDevice.Verify(device => device.Write(It.IsAny<byte[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void Call_GetModeColor_InvalidIndex_ReturnsBlack(int wrongIndex)
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            Assert.Equal(Color.Black, controller.GetModeColor(wrongIndex));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void Call_GetPortColor_InvalidIndex_ReturnsBlack(int wrongIndex)
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            Assert.Equal(Color.Black, controller.GetPortColor(wrongIndex));
        }

        [Fact]
        public async Task Call_ReadCurrentMode_CallsWriteAndRead()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            mockDevice.Setup(device => device.WriteAndRead(It.IsAny<byte[]>())).Returns(() => {
                var response = new byte[64];
                response[PacketOffset.Mode] = Mode.ColorCycle;

                return response;
            });
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.ReadCurrentModeAsync();

            mockDevice.Verify(device => device.WriteAndRead(It.IsAny<byte[]>()), Times.AtLeastOnce);
            Assert.Equal(Mode.ColorCycle, controller.Mode);
        }

        [Theory]
        [InlineData(0x3A, 0x3B, KnownColor.Red, KnownColor.Green)]
        [InlineData(0x6F, 0x8A, KnownColor.Green, KnownColor.Blue)]
        [InlineData(0xD3, 0xB3, KnownColor.Blue, KnownColor.White)]
        [InlineData(0xE9, 0xEA, KnownColor.White, KnownColor.Red)]
        public async Task Call_ReadModeConfig_CallsWriteAndRead(byte speed, byte brightness, KnownColor knownColor1, KnownColor knownColor2)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);

            var mockDevice = new Mock<ITestHidDevice>();
            mockDevice.Setup(device => device.WriteAndRead(It.IsAny<byte[]>())).Returns(() => {
                var response = new byte[64];
                response[PacketOffset.Speed] = speed;
                response[PacketOffset.Brightness] = brightness;
                response.SetColor(PacketOffset.Color1, color1);
                response.SetColor(PacketOffset.Color2, color2);

                return response;
            });
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.ReadModeConfigAsync(Mode.Star);

            mockDevice.Verify(device => device.WriteAndRead(It.IsAny<byte[]>()), Times.AtLeastOnce);
            Assert.Equal(Mode.Star, controller.Mode);
            Assert.Equal(speed, controller.Speed);
            Assert.Equal(brightness, controller.Brightness);
            Assert.Equal(color1.ToArgb(), controller.GetModeColor(0).ToArgb());
            Assert.Equal(color2.ToArgb(), controller.GetModeColor(1).ToArgb());
        }

        [Theory]
        [InlineData(KnownColor.Red, KnownColor.Green, KnownColor.Blue, KnownColor.White)]
        [InlineData(KnownColor.Green, KnownColor.Blue, KnownColor.White, KnownColor.Red)]
        [InlineData(KnownColor.Blue, KnownColor.White, KnownColor.Red, KnownColor.Green)]
        [InlineData(KnownColor.White, KnownColor.Red, KnownColor.Green, KnownColor.Blue)]
        public async Task Call_ReadModeConfigMultiple_CallsWriteAndRead(KnownColor knownColor1, KnownColor knownColor2, KnownColor knownColor3, KnownColor knownColor4)
        {
            var color1 = Color.FromKnownColor(knownColor1);
            var color2 = Color.FromKnownColor(knownColor2);
            var color3 = Color.FromKnownColor(knownColor3);
            var color4 = Color.FromKnownColor(knownColor4);

            var mockDevice = new Mock<ITestHidDevice>();
            mockDevice.Setup(device => device.WriteAndRead(It.IsAny<byte[]>())).Returns(() => {
                var response = new byte[64];
                response.SetColor(PacketOffset.MultipleColor1, color1);
                response.SetColor(PacketOffset.MultipleColor2, color2);
                response.SetColor(PacketOffset.MultipleColor3, color3);
                response.SetColor(PacketOffset.MultipleColor4, color4);

                return response;
            });
            var controller = new TestLedControllerDevice(mockDevice.Object);

            await controller.ReadModeConfigAsync(Mode.Multiple);

            mockDevice.Verify(device => device.WriteAndRead(It.IsAny<byte[]>()), Times.AtLeastOnce);
            Assert.Equal(color1.ToArgb(), controller.GetPortColor(0).ToArgb());
            Assert.Equal(color2.ToArgb(), controller.GetPortColor(1).ToArgb());
            Assert.Equal(color3.ToArgb(), controller.GetPortColor(2).ToArgb());
            Assert.Equal(color4.ToArgb(), controller.GetPortColor(3).ToArgb());
        }

        [Fact]
        public async Task Call_Dispose_ShouldDisposeDevice()
        {
            var mockDevice = new Mock<ITestHidDevice>();
            var controller = new TestLedControllerDevice(mockDevice.Object);

            controller.Dispose();

            mockDevice.Verify(device => device.Dispose(), Times.Once);
        }
    }
}
