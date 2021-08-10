using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Sander0542.CMLedController.Abstractions
{
    public interface ILedControllerDevice
    {
        public Task SendRequestAsync(IRequest request, CancellationToken token = default);
    }
}
