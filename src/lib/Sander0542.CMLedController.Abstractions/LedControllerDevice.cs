using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions.Enums;

namespace Sander0542.CMLedController.Abstractions
{
    public abstract class LedControllerDevice<TDevice> : ILedControllerDevice
    {
        public const int PacketSize = 64;

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

        protected abstract Task WriteAsync(byte[] data, CancellationToken token = default);

        protected abstract Task<byte[]> WriteAndReadAsync(byte[] data, CancellationToken token = default);

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

            if (mode == Mode.Multiple)
            {
                await SendReadCustomColorsAsync(token);
            }
        }

        private async Task SendFlowControlAsync(byte flag, CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[0] = OpCode.FlowControl;
            data[1] = flag;

            await WriteAsync(data, token);
        }

        private async Task SendApplyAsync(CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[0] = OpCode.Unknown50;
            data[1] = OpCodeType.Unknown55;

            await WriteAsync(data, token);
        }

        private async Task SendReadModeAsync(CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Read;
            data[PacketOffset.Type] = OpCodeType.Mode;

            var response = await WriteAndReadAsync(data, token);

            Mode = response[PacketOffset.Mode];
        }

        private async Task SendSetModeAsync(Mode mode, CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Write;
            data[PacketOffset.Type] = OpCodeType.Mode;
            data[PacketOffset.Mode] = mode;

            await WriteAsync(data, token);
        }

        private async Task SendSetCustomColorsAsync(Color color1, Color color2, Color color3, Color color4, CancellationToken token = default)
        {
            _port1Color = color1;
            _port2Color = color2;
            _port3Color = color3;
            _port4Color = color4;

            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Write;
            data[PacketOffset.Type] = OpCodeType.LedInfo;

            data[PacketOffset.MultipleColor1] = color1.R;
            data[PacketOffset.MultipleColor1 + 1] = color1.G;
            data[PacketOffset.MultipleColor1 + 2] = color1.B;

            data[PacketOffset.MultipleColor2] = color2.R;
            data[PacketOffset.MultipleColor2 + 1] = color2.G;
            data[PacketOffset.MultipleColor2 + 2] = color2.B;

            data[PacketOffset.MultipleColor3] = color3.R;
            data[PacketOffset.MultipleColor3 + 1] = color3.G;
            data[PacketOffset.MultipleColor3 + 2] = color3.B;

            data[PacketOffset.MultipleColor4] = color4.R;
            data[PacketOffset.MultipleColor4 + 1] = color4.G;
            data[PacketOffset.MultipleColor4 + 2] = color4.B;

            await WriteAsync(data, token);
        }

        private async Task SendReadCustomColorsAsync(CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Read;
            data[PacketOffset.Type] = OpCodeType.LedInfo;

            var response = await WriteAndReadAsync(data, token);

            _port1Color = Color.FromArgb(
                response[PacketOffset.MultipleColor1],
                response[PacketOffset.MultipleColor1 + 1],
                response[PacketOffset.MultipleColor1 + 2]
            );

            _port2Color = Color.FromArgb(
                response[PacketOffset.MultipleColor2],
                response[PacketOffset.MultipleColor2 + 1],
                response[PacketOffset.MultipleColor2 + 2]
            );

            _port3Color = Color.FromArgb(
                response[PacketOffset.MultipleColor3],
                response[PacketOffset.MultipleColor3 + 1],
                response[PacketOffset.MultipleColor3 + 2]
            );

            _port4Color = Color.FromArgb(
                response[PacketOffset.MultipleColor4],
                response[PacketOffset.MultipleColor4 + 1],
                response[PacketOffset.MultipleColor4 + 2]
            );
        }

        private async Task SendSetConfigAsync(Mode mode, byte speed, byte brightness, Color color1, Color color2, bool simplified = false, bool multilayer = false, CancellationToken token = default)
        {
            Mode = mode;
            Speed = speed;
            Brightness = brightness;
            _modeColor1 = color1;
            _modeColor2 = color2;

            if (mode == Mode.ColorCycle)
            {
                brightness = 0xDF;
                color1 = Color.White;
                color2 = Color.Black;
            }
            else if (mode == Mode.Off)
            {
                brightness = 0x03;
            }

            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Write;
            data[PacketOffset.Type] = simplified ? OpCodeType.ConfigSimplified : OpCodeType.ConfigFull;
            data[PacketOffset.Mode] = mode;
            data[PacketOffset.Speed] = speed;
            data[PacketOffset.Brightness] = brightness;

            data[PacketOffset.Color1] = color1.R;
            data[PacketOffset.Color1 + 1] = color1.G;
            data[PacketOffset.Color1 + 2] = color1.B;

            data[0x06] = (byte)(mode == Mode.Breathing ? 0x20 : 0x00);
            data[0x07] = (byte)(mode == Mode.Star ? 0x19 : 0xFF);
            data[0x08] = 0xFF;

            if (!simplified)
            {
                data[PacketOffset.Multilayer] = (byte)(multilayer ? 0x01 : 0x00);
                data[PacketOffset.Color2] = color2.R;
                data[PacketOffset.Color2 + 1] = color2.G;
                data[PacketOffset.Color2 + 2] = color2.B;

                for (var i = 16; i < data.Length; i++)
                {
                    data[i] = 0xFF;
                }
            }

            await WriteAsync(data, token);
        }

        private async Task SendReadConfigAsync(Mode mode, CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Read;
            data[PacketOffset.Type] = OpCodeType.ConfigFull;
            data[PacketOffset.Mode] = mode;

            var response = await WriteAndReadAsync(data, token);

            Mode = mode;
            Speed = response[PacketOffset.Speed];
            Brightness = response[PacketOffset.Brightness];

            _modeColor1 = Color.FromArgb(
                response[PacketOffset.Color1],
                response[PacketOffset.Color1 + 1],
                response[PacketOffset.Color1 + 2]
            );
            _modeColor2 = Color.FromArgb(
                response[PacketOffset.Color2],
                response[PacketOffset.Color2 + 1],
                response[PacketOffset.Color2 + 2]
            );
        }

        private async Task SendCustomColorStartAsync(CancellationToken token = default)
        {
            var data = new byte[PacketSize];
            data[PacketOffset.Op] = OpCode.Read;
            data[PacketOffset.Type] = OpCodeType.Unknown30;

            await WriteAsync(data, token);
        }

        protected byte[] PrepareData(byte[] data)
        {
            if (data.Length == PacketSize)
            {
                return new byte[] { 0x00 }.Concat(data).ToArray();
            }

            if (data.Length == PacketSize + 1 && data[0] == 0x00)
            {
                return data;
            }

            if (data.Length > PacketSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The data of the packet cannot be greater than {PacketSize}");
            }

            var newData = data.ToList();
            while (newData.Count < PacketSize)
            {
                newData.Add(0x00);
            }
            return new byte[] { 0x00 }.Concat(newData).ToArray();
        }
    }
}
