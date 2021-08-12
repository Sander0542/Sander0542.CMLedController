using System;
using System.Linq;
using Xunit;

namespace Sander0542.CMLedController.RealDevice.Tests.Attributes
{
    public sealed class IgnoreWhenNoDeviceFactAttribute : FactAttribute
    {
        public IgnoreWhenNoDeviceFactAttribute()
        {
            if (!LedControllerHelper.Present)
            {
                Skip = "This test requires at least one CoolerMaster RGB LED Controller installed";
            }
        }
    }

    public sealed class IgnoreWhenNoDeviceTheoryAttribute : TheoryAttribute
    {
        public IgnoreWhenNoDeviceTheoryAttribute()
        {
            if (!LedControllerHelper.Present)
            {
                Skip = "This test requires at least one CoolerMaster RGB LED Controller installed";
            }
        }
    }

    internal static class LedControllerHelper
    {
        internal static bool Present
        {
            get
            {
                try
                {
                    return new LedControllerProvider().GetControllersAsync().Result.Any();
                }
                catch (Exception)
                {

                }

                return false;
            }
        }
    }
}
