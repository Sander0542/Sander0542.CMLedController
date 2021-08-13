using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions.Enums;

namespace Sander0542.CMLedController.Abstractions
{
    public interface ILedControllerDevice : IDisposable
    {
        Mode Mode { get; }

        byte Speed { get; }

        byte Brightness { get; }

        Color GetModeColor(int colorIndex);

        Color GetPortColor(int portIndex);

        Task SetModeAsync(Mode mode, byte speed, byte brightness, Color color1, Color color2, CancellationToken token = default);

        Task SetLedsDirectAsync(Color color1, Color color2, Color color3, Color color4, CancellationToken token = default);

        Task ReadCurrentModeAsync(CancellationToken token = default);

        Task ReadModeConfigAsync(Mode mode, CancellationToken token = default);
    }
}
