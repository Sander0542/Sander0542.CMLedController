using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sander0542.CMLedController.Abstractions
{
    public interface ILedControllerProvider
    {
        public Task<IEnumerable<ILedControllerDevice>> GetControllersAsync(CancellationToken token = default);
    }
}
