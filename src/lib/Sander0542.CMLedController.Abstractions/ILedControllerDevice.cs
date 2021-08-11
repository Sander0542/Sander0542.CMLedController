using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Sander0542.CMLedController.Abstractions.Enums;

namespace Sander0542.CMLedController.Abstractions
{
    public interface ILedControllerDevice : IDisposable
    {
        string GetDeviceName();

        string GetSerial();

        string GetLocation();

        Mode GetMode();

        byte GetSpeed();

        byte GetBrightness();

        Color GetModeColor(int colorNumber);

        Color GetPortColor(int portNumber);

        Task SetModeAsync(Mode mode, byte speed, byte brightness, Color color1, Color color2, CancellationToken token = default);

        Task SetLedsDirectAsync(Color color1, Color color2, Color color3, Color color4, CancellationToken token = default);

        Task ReadCurrentModeAsync(CancellationToken token = default);

        Task ReadModeConfigAsync(Mode mode, CancellationToken token = default);
    }
}
