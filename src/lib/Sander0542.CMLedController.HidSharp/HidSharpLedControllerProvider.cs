using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HidSharp;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.HidSharp
{
    public class HidSharpLedControllerProvider : ILedControllerProvider
    {
        public async Task<IEnumerable<ILedControllerDevice>> GetControllersAsync(CancellationToken token = default)
        {
        }
    }
}
