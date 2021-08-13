using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions.Enums;
using Sander0542.CMLedController.Abstractions.Extensions;

namespace Sander0542.CMLedController.Abstractions
{
    public abstract class LedControllerDevice<TDevice> : ILedControllerDevice
    {
        protected TDevice Device;

        private Color _modeColor1;
        private Color _modeColor2;

        private Color _port1Color;
        private Color _port2Color;
        private Color _port3Color;
        private Color _port4Color;

        public LedControllerDevice(TDevice device)
        {
            Device = device;
        }

        protected abstract Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default);

        protected async Task<Packet> WriteAndReadDataAsync(Packet packet, CancellationToken token = default)
        {
            return (await WriteAndReadAsync(packet, token)).PrepareResponse();
        }

        public abstract void Dispose();

        public Mode Mode { get; private set; }

        public byte Speed { get; private set; }

        public byte Brightness { get; private set; }

        public virtual Color GetModeColor(int colorIndex)
        {
            return colorIndex switch
            {
                0 => _modeColor1,
                1 => _modeColor2,
                _ => Color.Black
            };
        }

        public virtual Color GetPortColor(int portIndex)
        {
            return portIndex switch
            {
                0 => _port1Color,
                1 => _port2Color,
                2 => _port3Color,
                3 => _port4Color,
                _ => Color.Black
            };
        }

        public virtual async Task SetModeAsync(Mode mode, byte speed, byte brightness, Color color1, Color color2, CancellationToken token = default)
        {
            await SendFlowControlAsync(OpCodeFlow.Flow01, token);

            await SendSetConfigAsync(mode, speed, brightness, color1, color2, false, token: token);

            await SendSetModeAsync(mode, token);

            await SendApplyAsync(token);

            await SendFlowControlAsync(OpCodeFlow.Flow00, token);
        }

        public virtual async Task SetLedsDirectAsync(Color color1, Color color2, Color color3, Color color4, CancellationToken token = default)
        {
            await SendFlowControlAsync(OpCodeFlow.Flow80, token);

            await SendCustomColorStartAsync(token);

            await SendSetCustomColorsAsync(color1, color2, color3, color4, token);

            await SendSetModeAsync(Mode.Multiple, token);

            await SendCustomColorStartAsync(token);

            await SendSetConfigAsync(Mode.Multiple, 0x00, 0xFF, color1, Color.White, false, token: token);

            await SendApplyAsync(token);

            await SendFlowControlAsync(OpCodeFlow.Flow00, token);
        }

        public virtual async Task ReadCurrentModeAsync(CancellationToken token = default)
        {
            await SendFlowControlAsync(OpCodeFlow.Flow01, token);

            await SendReadModeAsync(token);
        }

        public virtual async Task ReadModeConfigAsync(Mode mode, CancellationToken token = default)
        {
            await SendFlowControlAsync(OpCodeFlow.Flow00, token);

            await SendReadConfigAsync(mode, token);

            if (Equals(mode, Mode.Multiple))
            {
                await SendReadCustomColorsAsync(token);
            }
        }

        private async Task SendFlowControlAsync(byte flag, CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.FlowControl,
                OperationType = flag
            };

            await WriteAndReadDataAsync(packet, token);
        }

        private async Task SendApplyAsync(CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Unknown50,
                OperationType = OpCodeType.Unknown55
            };

            await WriteAndReadDataAsync(packet, token);
        }

        private async Task SendReadModeAsync(CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Read,
                OperationType = OpCodeType.Mode
            };

            var response = await WriteAndReadDataAsync(packet, token);

            Mode = response.Mode;
        }

        private async Task SendSetModeAsync(Mode mode, CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Write,
                OperationType = OpCodeType.Mode,
                Mode = mode
            };

            await WriteAndReadDataAsync(packet, token);
        }

        private async Task SendSetCustomColorsAsync(Color color1, Color color2, Color color3, Color color4, CancellationToken token = default)
        {
            _port1Color = color1;
            _port2Color = color2;
            _port3Color = color3;
            _port4Color = color4;

            var packet = new Packet
            {
                Operation = OpCode.Write,
                OperationType = OpCodeType.LedInfo,
                MultipleColor1 = color1,
                MultipleColor2 = color2,
                MultipleColor3 = color3,
                MultipleColor4 = color4,
            };

            await WriteAndReadDataAsync(packet, token);
        }

        private async Task SendReadCustomColorsAsync(CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Read,
                OperationType = OpCodeType.LedInfo
            };

            var response = await WriteAndReadDataAsync(packet, token);

            _port1Color = response.MultipleColor1;
            _port2Color = response.MultipleColor2;
            _port3Color = response.MultipleColor3;
            _port4Color = response.MultipleColor4;
        }

        private async Task SendSetConfigAsync(Mode mode, byte speed, byte brightness, Color color1, Color color2, bool simplified = false, bool multilayer = false, CancellationToken token = default)
        {
            Mode = mode;
            Speed = speed;
            Brightness = brightness;
            _modeColor1 = color1;
            _modeColor2 = color2;

            if (Equals(mode, Mode.ColorCycle))
            {
                brightness = 0xDF;
                color1 = Color.White;
                color2 = Color.Black;
            }
            else if (Equals(mode, Mode.Off))
            {
                brightness = 0x03;
            }

            var packet = new Packet
            {
                Operation = OpCode.Write,
                OperationType = simplified ? OpCodeType.ConfigSimplified : OpCodeType.ConfigFull,
                Mode = mode,
                Speed = speed,
                Brightness = brightness,
                Color1 = color1
            };
            packet.Set(0x06, (byte)(Equals(mode, Mode.Breathing) ? 0x20 : 0x00));
            packet.Set(0x07, (byte)(Equals(mode, Mode.Star) ? 0x19 : 0xFF));
            packet.Set(0x08, 0xFF);

            if (!simplified)
            {
                packet.Multilayer = (byte)(multilayer ? 0x01 : 0x00);
                packet.Color2 = color2;

                for (var i = 16; i < packet.Length; i++)
                {
                    packet.Set(i, 0xFF);
                }
            }

            await WriteAndReadDataAsync(packet, token);
        }

        private async Task SendReadConfigAsync(Mode mode, CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Read,
                OperationType = OpCodeType.ConfigFull,
                Mode = mode
            };

            var response = await WriteAndReadDataAsync(packet, token);

            Mode = mode;
            Speed = response.Speed;
            Brightness = response.Brightness;

            _modeColor1 = response.Color1;
            _modeColor2 = response.Color2;
        }

        private async Task SendCustomColorStartAsync(CancellationToken token = default)
        {
            var packet = new Packet
            {
                Operation = OpCode.Read,
                OperationType = OpCodeType.Unknown30
            };

            await WriteAndReadDataAsync(packet, token);
        }
    }
}
